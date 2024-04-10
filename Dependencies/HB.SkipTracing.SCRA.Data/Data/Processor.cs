using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace HB.SkipTracing.SCRA.Data
{
    public class Processor
    {
        public event Action<int, int, int> ProgressUpdated;
        public List<SkipTracing.Data.RNN.Records.MilitaryRecord> Records { get; private set; } = new 
        List<SkipTracing.Data.RNN.Records.MilitaryRecord>();
          public async Task ProcessDownloadsAsync(System.IO.FileInfo file, System.IO.DirectoryInfo saveDirectory)
        {
            List<Failure> failures = new List<Failure>();
            var match = System.Text.RegularExpressions.Regex.Match(file.Name, Settings.Properties.Naming_Hits);
            if (match.Success)
            {
                DateTime batchDate = DateTime.ParseExact(match.Groups["datetime"].Value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                string batchId = match.Groups["caseNo"].Value;
                string clientId = match.Groups["clientId"].Value;
                try
                {
                    bool fileFound = false;
                    SkipTracing.Data.RNN.FileReader fileReader = new SkipTracing.Data.RNN.FileReader();
                    var records = await fileReader.ReadReturnFileAsync(file);
                    if (records != null && records.Count() > 0)
                    {
                        // Add Records to Statically Typed List for Processing (Both Here and In Uploads)
                        Records.AddRange(records.OfType<SkipTracing.Data.RNN.Records.MilitaryRecord>().Where(rec => rec.ActiveDutyStatus));

                        // Download PDFs
                        var login = await RemoteFile.Connections.SiteConnectionManager.FindLoginByNameAsync("RNN");
                        using (var connection = login.GetClient())
                        {
                            connection.Connect();
                            connection.ChangeDirectory(Settings.Properties.Location_Certificates);
                            // Get Listing of Remote Files
                            var files = connection.FileListing(Settings.Properties.Location_Certificates);
                            foreach (var f in files)
                            {
                                // Check File Name
                                var fMatch = System.Text.RegularExpressions.Regex.Match(f.Name, Settings.Properties.Naming_Certificates);
                                if (fMatch.Success)
                                {
                                    // Check Batch ID
                                    var fBatchId = fMatch.Groups["caseNo"].Value;
                                    if (string.Equals(fBatchId, batchId))
                                    {
                                        // Batch Matches Current File - Download
                                        connection.GetFile(f, System.IO.Path.Combine(Settings.Properties.SaveLocation_Downloads, f.Name), new RemoteFile.Delegates.FileTransferEventHandler((o, ev)=> { this.ProgressUpdated?.Invoke((ev.PercentComplete ?? 0) == -1 ? Convert.ToInt32(ev.Transferred * 100 / (ev.Total == 0 ? 10000000 : ev.Total)) : (ev.PercentComplete ?? 0), 1, 2); }));
                                        connection.Delete(f);
                                        fileFound = true;
                                        break;
                                    }
                                }
                            }
                        }
                        // Get Listing of Accounts to Code
                        var accountsNeedingCoding = await Accounts.DataHandler.Get_AccountList_NeedingCodingAsync();
                        // Generate XCODES
                        List<RecordTypes.MergePops.MergePop> mergePops = new List<RecordTypes.MergePops.MergePop>(Records.Select(hit => new RecordTypes.MergePops.MergePop()
                        {
                            // Hit Recorded
                            FILENO = hit.FileNo,
                            LLCODE = $"XSCRA-H{hit.DebtorNo}"
                        }));
                        mergePops.AddRange(accountsNeedingCoding.Where(anc => !this.Records.Select(r => r.FileNo).Contains(anc, StringComparer.CurrentCultureIgnoreCase)).Select(anc => new RecordTypes.MergePops.MergePop()
                        {
                            // No Hit Recorded
                            FILENO = anc,
                            LLCODE = "XSCRA-S"
                        }));
                        // Generate Notes
                        List<RecordTypes2.YGC.RecordType09> notes = new List<RecordTypes2.YGC.RecordType09>(Records.Select(hit => new RecordTypes2.YGC.RecordType09()
                        {
                            MASCO_FILE = hit.FileNo,
                            PCMT = "SCRA On Active Duty",
                            NOTE02 = $"Debtor: {hit.DebtorNo}",
                            NOTE03 = $"Certificate: {hit.ReportID}"
                        }));
                        notes.AddRange(accountsNeedingCoding.Where(anc => !this.Records.Select(r => r.FileNo).Contains(anc, StringComparer.CurrentCultureIgnoreCase)).SelectMany(anc =>
                            records.OfType<SkipTracing.Data.RNN.Records.MilitaryRecord>().Where(rec => rec.FileNo.Equals(anc, StringComparison.OrdinalIgnoreCase)).Select(result =>
                                new RecordTypes2.YGC.RecordType09()
                                {
                                    MASCO_FILE = result.FileNo,
                                    PCODE = "XCA",
                                    PCMT = "SCRA Non-Active Duty",
                                    NOTE02 = $"Debtor: {result.DebtorNo}",
                                    NOTE03 = $"Certificate: {result.ReportID}"
                                })
                            )
                        );

                        // Search Download Folder for PDF Zip File
                        var downloadFolder = new System.IO.DirectoryInfo(Settings.Properties.SaveLocation_Downloads);
                        var zipFile = downloadFolder.EnumerateFiles("*.zip", System.IO.SearchOption.TopDirectoryOnly).FirstOrDefault(f =>
                        {
                            var m = System.Text.RegularExpressions.Regex.Match(f.Name, Settings.Properties.Naming_Certificates);
                            return m.Groups["caseNo"].Value.Equals(batchId);
                        });

                        if (!fileFound && zipFile == null)
                        {
                            // We're Done - They Haven't Uploaded the File Yet Or We Don't Have it Locally
                            throw new Exception("The Certificates Zip File Could Not Be Located\r\n\r\nEither The File Hasn't Been Uploaded by RNN\r\nOr We Have Deleted Our Local Copy");
                        }

                        List<RecordTypes.MergePops.MergePop> mergesToRemove = new List<RecordTypes.MergePops.MergePop>();
                        await Task.Run(async () =>
                        {
                            using (var zipArchive = Zip.ZipArchive.OpenOnFile(zipFile.FullName))
                            {
                                // Get PDFs and Save to \\Soter\Mil_Scrubs as {SSN}.pdf for import
                                for (int i = 0; i < mergePops.Count; i++)
                                {
                                    // Update Progress
                                    this.ProgressUpdated?.Invoke(i * 100 / mergePops.Count, 2, 2);

                                    var fNo = mergePops[i].FILENO;
                                    var matchDeb = System.Text.RegularExpressions.Regex.Match(mergePops[i].LLCODE, @"XSCRA\-[a-zA-Z]{1}(?<debtor>[\d]{1})");
                                    if (matchDeb.Success)
                                    {
                                        var deb = matchDeb.Groups["debtor"].Value;

                                        var certFile = await Task.Run(() => { return zipArchive.Files.FirstOrDefault(zip => zip.Name.StartsWith($"{fNo}-{deb}", StringComparison.OrdinalIgnoreCase)); });

                                        if (certFile == default(Zip.ZipArchive.ZipFileInfo))
                                        {
                                            //  We Got No Certificate!!!!!!  HUGE PROBLEM - WE GOT A HIT, BUT NO CERT!

                                            // Start by removing update records
                                            mergesToRemove.Add(mergePops[i]);
                                            // Then Report the Problem
                                            failures.Add(new Failure()
                                            {
                                                FileNo = fNo,
                                                Debtor = deb,
                                                Error = "We Received a Positive Hit, But No Certificate Found in File"
                                            });
                                        }
                                        else
                                        {
                                            await DataHandler.SaveScrubFileToScan(fNo, deb, certFile);
                                        }
                                    }
                                    else
                                    {
                                        int cntCert = 0;
                                        int cntCertMatch = 0;
                                        // Find All Debtors
                                        foreach (var certFile in await Task.Run(() => { return zipArchive.Files.Where(zip => zip.Name.StartsWith($"{fNo}", StringComparison.OrdinalIgnoreCase)); }))
                                        {
                                            cntCert++;
                                            var debMatch = System.Text.RegularExpressions.Regex.Match(certFile.Name, @"(?<fileNo>[a-zA-Z0-9\.]{1,9})\-(?<debtor>[0-9]{1})\-[\d]+");
                                            if (debMatch.Success)
                                            {
                                                var deb = debMatch.Groups["debtor"].Value;

                                                await DataHandler.SaveScrubFileToScan(fNo, deb, certFile);
                                                cntCertMatch++;
                                            }
                                        }

                                        if (cntCert != cntCertMatch || cntCert == 0 || cntCertMatch == 0)
                                        {
                                            // We Got No Certificate!!!!!!

                                            // Start by removing update records
                                            mergesToRemove.Add(mergePops[i]);
                                            // Then Report the Problem
                                            failures.Add(new Failure()
                                            {
                                                FileNo = fNo,
                                                Debtor = "ALL",
                                                Error = $"We Need to Code This File \"{mergePops[i].LLCODE}\", But No Certificate or Invalid Number of Certificates Was Returned ({cntCert} Found / {cntCertMatch} Matched)"
                                            });
                                        }
                                    }
                                }
                            }
                        });

                        // Remove Failed Merges
                        mergePops.RemoveAll(m => mergesToRemove.Contains(m));

                        // Export Update Files
                        using (RecordTypes.MergePops.FileWriter writer = new RecordTypes.MergePops.FileWriter(System.IO.Path.Combine(saveDirectory.FullName, $"SCRA_Merges_{DateTime.Now:yyyyMMdd-HHmmss}.txt")))
                        {
                            writer.WriteFile(mergePops);
                        }
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(saveDirectory.FullName, $"SCRA_YGC_{DateTime.Now:yyyyMMdd-HHmmss}.txt")))
                        {
                            foreach (var note in notes)
                            {
                                await sw.WriteLineAsync(note.ToString());
                            }
                            await sw.FlushAsync();
                        }

                        // Generate Updates for SQL
                        await DataHandler.UpdateAccountsAsync(Records);

                        // Generate Failures Report
                        if (failures.Count > 0)
                        {
                            await CreateFailuresReport(failures);
                        }

                        // Move Zip to Processed Folder
                        try
                        {
                            System.IO.DirectoryInfo processedDir = new System.IO.DirectoryInfo(System.IO.Path.Combine(Settings.Properties.SaveLocation_Downloads, "Processed"));
                            zipFile.MoveTo(System.IO.Path.Combine(processedDir.FullName, zipFile.Name));
                            // Clean Up Processed Folder (Leave Latest 3 Zips)
                            try
                            {
                                for (int i = 0; i < (processedDir.EnumerateFiles("*.zip").Count() - 3); i++)
                                {
                                    var zipToDelete = processedDir.EnumerateFiles("*.zip").OrderBy(f => f.CreationTime).FirstOrDefault();
                                    if (zipToDelete != null)
                                    {
                                        zipToDelete.Delete();
                                    }
                                }
                            }
                            catch(Exception ex)
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Unable to Move Zip File\r\n\r\nFrom: {zipFile.FullName}\r\nTo: {System.IO.Path.Combine(Settings.Properties.SaveLocation_Downloads, "Processed", zipFile.Name)}\r\n\r\n{ex.Message}", ex);
                        }
                    }
                    else
                    {
                        // Nothing to Do or Problem?
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else
            {
                throw new InvalidOperationException("Unexpected File Name\r\n\r\nFile Name Must Contain Date and Batch Info\r\nEx: YYYYMMDDHHMMSS-12345-1279-Military Hits.csv\r\n\r\nUnable to Continue");
            }
        }
        public async Task GenerateUploadsAsync(System.IO.DirectoryInfo saveDirectory, Func<string, string, System.Windows.MessageBoxButton, System.Windows.MessageBoxImage, System.Windows.MessageBoxResult> dialog)
        {
            if ((this.Records?.Count ?? 0) == 0)
            {
                if (dialog?.Invoke("Download Processing Must Occur Before Uploads\r\n\r\nHave You Completed Downloads?", "Warning!", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.No)
                {
                    return;
                }
            }

            // Get Upload Additions
            var additionAccounts = await Data.Accounts.DataHandler.Get_UploadAccounts_AddAsync();
            // Get Upload Removals
            var removalAccounts = new List<Data.Accounts.AccountDebtor>((await Data.Accounts.DataHandler.Get_UploadAccounts_RemoveAsync()) ?? new List<Data.Accounts.AccountDebtor>());
            // Get Upload Removals Based on Hits in "this.Records"
            removalAccounts.AddRange((await Data.Accounts.DataHandler.Get_UploadAccounts_AccountListAsync(this.Records?.Select(hit => new Tuple<string, byte>(hit.FileNo, hit.DebtorNo)))) ?? new List<Data.Accounts.AccountDebtor>());

            // Generate Upload Files Add
            SkipTracing.Data.RNN.FileReader fileWriter = new SkipTracing.Data.RNN.FileReader();
            if (additionAccounts?.Count() > 0)
            {
                await fileWriter.CreateUploadFileAsync(new System.IO.FileInfo(System.IO.Path.Combine(saveDirectory.FullName, $"SCRA_ADD_{DateTime.Now:yyyyMMdd-HHmmss}.csv")), additionAccounts.Select(aa => new Records.UploadRecord(aa)), (progress) => this.ProgressUpdated?.Invoke(progress, 1, 2));
                // Update SQL Notating Accounts Added to Warehouse
                await DataHandler.AddAccountsAsync(additionAccounts.Select(add => new Tuple<string, byte>(add.FILENO, add.NUMBER)));
            }
            // Generate Upload Files Remove
            if (removalAccounts.Count > 0)
            {
                await fileWriter.CreateUploadFileAsync(new System.IO.FileInfo(System.IO.Path.Combine(saveDirectory.FullName, $"SCRA_REMOVE_{DateTime.Now:yyyyMMdd-HHmmss}.csv")), removalAccounts.Select(aa => new Records.UploadRecord(aa)), (progress) => this.ProgressUpdated?.Invoke(progress, 2, 2));
                // Update SQL Notating Accounts Removed from Warehouse
                await DataHandler.RemoveAccountsAsync(removalAccounts.Select(rem => new Tuple<string, byte>(rem.FILENO, rem.NUMBER)));
            }
        }

        public async Task GenerateUploadsListAsync(IEnumerable<Tuple<string, byte>> accounts, System.IO.DirectoryInfo saveDirectory, Func<string, string, System.Windows.MessageBoxButton, System.Windows.MessageBoxImage, System.Windows.MessageBoxResult> dialog)
        {

            // Get Upload Additions
            var additionAccounts = await Data.Accounts.DataHandler.Get_UploadAccounts_AccountListAsync(accounts);

            // Generate Upload Files Add
            SkipTracing.Data.RNN.FileReader fileWriter = new SkipTracing.Data.RNN.FileReader();
            if (additionAccounts?.Count() > 0)
            {
                await fileWriter.CreateUploadFileAsync(new System.IO.FileInfo(System.IO.Path.Combine(saveDirectory.FullName, $"SCRA_ADD_{DateTime.Now:yyyyMMdd-HHmmss}.csv")), additionAccounts.Select(aa => new Records.UploadRecord(aa)), (progress) => this.ProgressUpdated?.Invoke(progress, 1, 2));
                // Update SQL Notating Accounts Added to Warehouse
                await DataHandler.AddAccountsAsync(additionAccounts.Select(add => new Tuple<string, byte>(add.FILENO, add.NUMBER)));
            }
        }
        public async Task CreateFailuresReport(List<Failure> failures)
        {
            ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel();
            xlApp.xlBook.AddWorksheetFromList("Failures", failures);
            xlApp.ShowWorkBook();
        }
        
    }
}
