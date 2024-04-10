using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.SkipTracing.Data.RNN.Records;

namespace HB.SkipTracing.Data.RNN
{
    public class FileReader : Data.Interfaces.IFileReader<Data.Records.UploadRecord, Data.Records.DownloadRecord>
    {
        public void CreateUploadFile(FileInfo file, IEnumerable<Data.Records.UploadRecord> uploadRecords, Action<int> progress = null)
        {
            using (ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel())
            {
                var dt = GenerateDataTable();
                foreach (var record in uploadRecords)
                {
                    AddUploadRecordToTable(dt, record as UploadRecord);
                }
                if (progress != null) xlApp.xlBook.ProgressUpdated += progress;
                xlApp.xlBook.AddWorksheetFromTable(dt);
                xlApp.CloseAndSaveAs(file.FullName, ExcelInterface.Enums.FileFormat.txt_csv);
            }
        }
        public Task CreateUploadFileAsync(FileInfo file, IEnumerable<Data.Records.UploadRecord> uploadRecords, Action<int> progress = null)
        {
            return Task.Run(() => CreateUploadFile(file, uploadRecords, progress));
        }

        public IEnumerable<Data.Records.DownloadRecord> ReadReturnFile(FileInfo file)
        {
            List<DownloadRecord> records = new List<DownloadRecord>();
            using (ExcelInterface.Application.Excel xlApp = new ExcelInterface.Application.Excel(file.FullName))
            {
                var dict = xlApp.xlBook.ActiveSheet.ReadDictionary();
                if (dict.FirstOrDefault().Keys.Contains("rnn_bank_name"))
                {
                    foreach (var d in dict)
                    {
                        records.Add(new BankRecord(d));
                    }
                }
                else if (dict.FirstOrDefault().Keys.Contains("ACTIVEDUTYSTATUS"))
                {
                    foreach (var d in dict)
                    {
                        records.Add(new MilitaryRecord(d));
                    }
                }
                else
                {
                    foreach (var d in dict)
                    {
                        records.Add(new EmployerRecord(d));
                    }
                }
                xlApp.CloseExcel();
            }
            return records;
        }
        public async Task<IEnumerable<Data.Records.DownloadRecord>> ReadReturnFileAsync(FileInfo file)
        {
            return await Task.Run(() => ReadReturnFile(file).ToArray());
        }

        private DataTable GenerateDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("social_security_number", typeof(string));
            dt.Columns.Add("first_name", typeof(string));
            dt.Columns.Add("last_name", typeof(string));
            dt.Columns.Add("street_address_1", typeof(string));
            dt.Columns.Add("street_address_2", typeof(string));
            dt.Columns.Add("city", typeof(string));
            dt.Columns.Add("state", typeof(string));
            dt.Columns.Add("zip_code", typeof(string));
            dt.Columns.Add("date_of_birth", typeof(string));
            dt.Columns.Add("phone_1", typeof(string));
            dt.Columns.Add("phone_2", typeof(string));
            dt.Columns.Add("phone_3", typeof(string));
            dt.Columns.Add("client_field_1", typeof(string));
            dt.Columns.Add("client_field_2", typeof(string));
            dt.Columns.Add("client_field_3", typeof(string));
            dt.Columns.Add("client_field_4", typeof(string));
            dt.Columns.Add("client_field_5", typeof(string));
            dt.Columns.Add("account_open_date", typeof(string));
            dt.Columns.Add("rnn_id", typeof(string));
            return dt;
        }
        private void AddUploadRecordToTable(DataTable dt, UploadRecord record)
        {
            var dr = dt.NewRow();
            dr["social_security_number"] = record.SSN;
            dr["first_name"] = record.First_Name;
            dr["last_name"] = record.Last_Name;
            dr["street_address_1"] = record.Address1;
            dr["street_address_2"] = record.Address2;
            dr["city"] = record.City;
            dr["state"] = record.State;
            dr["zip_code"] = record.Zip;
            dr["date_of_birth"] = record.Date_Of_Birth?.Date ?? (object)DBNull.Value;
            dr["phone_1"] = record.Phone1;
            dr["phone_2"] = record.Phone2;
            dr["phone_3"] = record.Phone3;
            dr["client_field_1"] = record.ClientField1;
            dr["client_field_2"] = record.ClientField2;
            dr["client_field_3"] = record.ClientField3;
            dr["client_field_4"] = record.ClientField4;
            dr["client_field_5"] = record.ClientField5;
            dr["account_open_date"] = record.OpenDate?.ToString("yyyyMMdd") ?? (object)DBNull.Value;
            dr["rnn_id"] = record.RNN_ID;
            dt.Rows.Add(dr);
        }
    }
}
