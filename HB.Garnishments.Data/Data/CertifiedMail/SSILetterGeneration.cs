using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.composition;
using org.pdfclown.documents.contents.entities;
using org.pdfclown.documents.contents.fonts;
using org.pdfclown.documents.contents.xObjects;
using org.pdfclown.documents.interaction.annotations;
using org.pdfclown.files;
using IDAutomation_FontEncoder;
using System.Threading.Tasks;
using org.pdfclown.bytes;

namespace HB.Garnishments.Data.CertifiedMail
{
    public static class SSILetterGenerator
    {
        public static async Task<System.IO.Stream> GenerateLettersAsync(IEnumerable<SSILetter> letters)
        {
            if (letters.SelectMany(l => l.Recipients).Count() > 0)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                org.pdfclown.bytes.Stream save = new org.pdfclown.bytes.Stream(ms);
                org.pdfclown.bytes.Stream letterHead = new org.pdfclown.bytes.Stream((await Data.DataHandler.GetFirmLetterHeadAsync()).GetDataStream());
                File TemplateFile = new File(letterHead);
                Document TemplateDocument = TemplateFile.Document;
                Page TemplatePage = TemplateDocument.Pages.Single();

                using (File file = new File())
                {
                    Document d = file.Document;
                    d.PageSize = PageFormat.GetSize(PageFormat.SizeEnum.Letter);

                    foreach (SSILetter letter in letters)
                    {
                        var clerk = Settings.Properties.Venues.FirstOrDefault(clk => clk.VenueNo == letter.Account.Venue);
                        var debtor1 = letter.Account.Debtors.FirstOrDefault(deb => deb.Debtor == 1);
                        var debtor2 = letter.Account.Debtors.FirstOrDefault(deb => deb.Debtor == 2);
                        foreach (var recipient in letter.Recipients.Where(rec => rec.ReceivesSSILetter))
                        {
                            Page p;
                            PrimitiveComposer pc;
                            BlockComposer bc;

                            p = new Page(d);
                            d.Pages.Add(p);

                            pc = new PrimitiveComposer(p);
                            pc.ShowXObject(TemplatePage.ToXObject(d));
                            pc.SetFont(new StandardType1Font(d, StandardType1Font.FamilyEnum.Times, false, false), 12);
                            pc.ShowText(DateTime.Today.ToString("MMMM dd, yyyy"), new PointF(70, 170));
                            pc.ShowText("Attn: ", new PointF(70, 200));

                            bc = new BlockComposer(pc);
                            bc.Begin(new RectangleF(120, 200, 260, 60), AlignmentXEnum.Left, AlignmentYEnum.Top);

                            bc.ShowText($"{(!string.IsNullOrEmpty(recipient.Name2) ? $"{recipient.Name2}\r\n" : "")}{recipient.Name1}\r\n{recipient.Address1}\r\n{(!string.IsNullOrEmpty(recipient.Address2) ? $"{recipient.Address2}\r\n" : "")}{recipient.City}, {recipient.State} {recipient.Zip}");
                            bc.End();

                            pc.ShowText("Our File No: ", new PointF(70, 260));
                            pc.ShowText(letter.Account.FileNo, new PointF(140, 260));
                            pc.Flush();

                            pc.SetFont(new StandardType1Font(d, StandardType1Font.FamilyEnum.Times, true, false), 12);
                            bc = new BlockComposer(pc);
                            bc.Begin(new RectangleF(70, 320, 440, 26), AlignmentXEnum.Left, AlignmentYEnum.Top);
                            bc.ShowText($"Please fax a copy of your file-marked answer to {Settings.Properties.FaxNumber} and send a copy of your answer to the address below.");
                            bc.End();
                            pc.Flush();

                            pc.SetFont(new StandardType1Font(d, StandardType1Font.FamilyEnum.Times, false, false), 12);
                            bc = new BlockComposer(pc);
                            bc.Begin(new RectangleF(140, 356, 320, 60), AlignmentXEnum.Left, AlignmentYEnum.Top);

                            bc.ShowText($"{clerk.Name?.ToUpper()}\r\n{clerk.Address1?.ToUpper()}\r\n{clerk.CityStateZip?.ToUpper()}");
                            bc.End();
                            pc.Flush();

                            pc.ShowText("Defendant: ", new PointF(90, 440));
                            pc.ShowText(debtor1?.DisplayName?.ToUpper() ?? "", new PointF(150, 440));
                            pc.ShowText("Social Security Number: ", new PointF(90, 464));
                            pc.ShowText(debtor1?.SSN ?? "", new PointF(220, 464));
                            if (debtor2 != null)
                            {
                                pc.ShowText("Defendant: ", new PointF(90, 488));
                                pc.ShowText(debtor2?.DisplayName?.ToUpper() ?? "", new PointF(150, 488));
                                pc.ShowText("Social Security Number: ", new PointF(90, 512));
                                pc.ShowText(debtor2?.SSN ?? "", new PointF(220, 512));
                            }

                            pc.ShowText("Remit payments to address below.", new PointF(90, 584));
                            pc.Flush();
                        }
                    }

                    file.Save(save, SerializationModeEnum.Standard);
                }
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return ms;
            }
            return new System.IO.MemoryStream();
        }
    }
}