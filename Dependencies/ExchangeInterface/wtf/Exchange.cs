using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Net;

namespace ExchangeInterface
{
    public class Exchange : IDisposable
    {
        private ExchangeService Service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);

        public Exchange()
        {
            UserPrincipal currentUser = UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain, "HOSTOBUCHAN"), WindowsIdentity.GetCurrent().Name);
            Service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            Service.TraceEnabled = true;
            Service.UseDefaultCredentials = true;
            //Service.AutodiscoverUrl(currentUser.EmailAddress);
            Service.Url = new Uri("https://apollo.hostobuchan.dom/EWS/exchange.asmx");
            //IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            //foreach (IPAddress ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        byte[] ipBytes = ip.GetAddressBytes();
            //        if (ipBytes[2] != 14 && ipBytes[2] != 0)
            //        {
            WebProxy proxy = new WebProxy(WebRequest.DefaultWebProxy.GetProxy(Service.Url), true) { Credentials = WebRequest.DefaultWebProxy.Credentials };
            Service.WebProxy = proxy;
            //        }
            //    }
            //}
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
        }

        public void SendEmail(string Subject, string Body, string[] Recipients, string[] CcRecipients, string[] Attachments, bool SaveCopy = false)
        {
            EmailMessage message = new EmailMessage(Service);

            message.Subject = Subject;
            message.Body = Body;
            if (Attachments != null)
            {
                foreach (string File in Attachments)
                {
                    message.Attachments.AddFileAttachment(File);
                }
            }
            foreach (string Recipient in Recipients)
            {
                message.ToRecipients.Add(Recipient);
            }
            if (CcRecipients != null)
            {
                foreach (string Recipient in CcRecipients)
                {
                    message.CcRecipients.Add(Recipient);
                }
            }
            if (SaveCopy)
            {
                message.SendAndSaveCopy();
            }
            else
            {
                message.Send();
            }
        }

        public void SendEmail(string Subject, string Body, string[] Recipients, string[] CcRecipients = null, Attachment[] Attachments = null, bool SaveCopy = false)
        {
            EmailMessage message = new EmailMessage(Service);

            message.Subject = Subject;
            message.Body = Body;
            if (Attachments != null)
            {
                foreach (Attachment ATT in Attachments)
                {
                    message.Attachments.AddFileAttachment(ATT.Name, ATT.Content);
                }
            }
            foreach (string Recipient in Recipients)
            {
                message.ToRecipients.Add(Recipient);
            }
            if (CcRecipients != null)
            {
                foreach (string Recipient in CcRecipients)
                {
                    message.CcRecipients.Add(Recipient);
                }
            }
            if (SaveCopy)
            {
                message.SendAndSaveCopy();
            }
            else
            {
                message.Send();
            }
        }

        public void SetCredentials(string Username, string Password)
        {
            Service.UseDefaultCredentials = false;
            Service.Credentials = new NetworkCredential(Username, Password, "HOSTOBUCHAN");
            //Service.WebProxy = new WebProxy("http://tmg1:8080", true) { UseDefaultCredentials = false, Credentials = new NetworkCredential(Username, Password, "HOSTOBUCHAN") };
        }

        /// <summary>
        /// Create Calendar Appointment using EWS
        /// </summary>
        /// <param name="Subject">Email Subject</param>
        /// <param name="StartTime">Date of Appointment (Start Time)</param>
        /// <param name="EndTime">Date of Appointment (End Time)</param>
        /// <param name="RequiredAttendees">Required Attendees</param>
        /// <param name="Body">Email Message Body</param>
        /// <param name="Location">Appointment Location</param>
        /// <returns>Base64 String EWS Unique ID</returns>
        public byte[] CreateCalendarEvent(string Subject, DateTime StartTime, DateTime EndTime, string[] RequiredAttendees, string Body = "", string Location = "")
        {
            Appointment A = new Appointment(Service)
            {
                Subject = Subject,
                Body = Body,
                Start = StartTime,
                End = EndTime,
                Location = Location
            };

            if (RequiredAttendees != null)
            {
                foreach (string RA in RequiredAttendees)
                {
                    A.RequiredAttendees.Add(RA);
                }
                A.Save(SendInvitationsMode.SendOnlyToAll);
                return Convert.FromBase64String(A.Id.UniqueId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create Calendar Appointment using EWS
        /// </summary>
        /// <param name="Subject">Email Subject</param>
        /// <param name="StartTime">Date of Appointment (Start Time)</param>
        /// <param name="EndTime">Date of Appointment (End Time)</param>
        /// <param name="RequiredAttendees">Required Attendees</param>
        /// <param name="OptionalAttendees">Optional Attendees</param>
        /// <param name="Body">Email Message Body</param>
        /// <param name="Location">Appointment Location</param>
        /// <returns>Base64 String EWS Unique ID</returns>
        public byte[] CreateCalendarEvent(string Subject, DateTime StartTime, DateTime EndTime, string[] RequiredAttendees, string[] OptionalAttendees, string Body = "", string Location = "")
        {
            Appointment A = new Appointment(Service)
            {
                Subject = Subject,
                Body = Body,
                Start = StartTime,
                End = EndTime,
                Location = Location
            };


            if (RequiredAttendees != null)
            {
                foreach (string RA in RequiredAttendees)
                {
                    A.RequiredAttendees.Add(RA);
                }
                if (OptionalAttendees != null)
                {
                    foreach (string OA in OptionalAttendees)
                    {
                        A.OptionalAttendees.Add(OA);
                    }
                }
                A.Save(SendInvitationsMode.SendOnlyToAll);
                return Convert.FromBase64String(A.Id.UniqueId);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Cancel Calendar Appointment using EWS
        /// </summary>
        /// <param name="UniqueID">Base64 String EWS Unique ID</param>
        /// <param name="CancellationMessage">Email Message Body</param>
        public void CancelCalendarEvent(byte[] UniqueID, string CancellationMessage = "")
        {
            Appointment A = Appointment.Bind(Service, new ItemId(Convert.ToBase64String(UniqueID)));
            if (!string.IsNullOrEmpty(CancellationMessage))
            {
                A.CancelMeeting(CancellationMessage);
            }
            else
            {
                A.CancelMeeting();
            }
        }

        public class Attachment
        {
            public string Name { get; set; }
            public System.IO.Stream Content { get; set; }

            public Attachment(string Name, System.IO.Stream Content)
            {
                this.Name = Name;
                this.Content = Content;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Service = null;
        }

        #endregion
    }
}
