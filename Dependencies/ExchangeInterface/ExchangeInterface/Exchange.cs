using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using Microsoft.Identity.Client;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Configuration;
namespace ExchangeInterface
{
    public class Exchange : IDisposable
    {
        public event Func<Exception, NetworkCredential> UnAuthorizedException;

        protected NetworkCredential OnUnAuthorizedException(Exception ex)
        {
            var credentials = new NetworkCredential(RegistryHandler.ReadProtectedString("Email"), RegistryHandler.ReadProtectedString("PasswordHash"));
            if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrEmpty(credentials.Password) || (this.Credentials != null && this.Credentials.UserName.Equals(credentials.UserName) && this.Credentials.Password.Equals(credentials.Password)))
            {
                credentials = UnAuthorizedException?.Invoke(ex);
                if (credentials != null)
                {
                    RegistryHandler.WriteProtectedString("Email", credentials.UserName);
                    RegistryHandler.WriteProtectedString("PasswordHash", credentials.Password);
                }
            }
            return credentials;
        }

        private string _Email;
        private NetworkCredential _Credentials;
        private NetworkCredential Credentials { get { return _Credentials; } set { _Credentials = value; this.Service.Credentials = new WebCredentials(value); } }

        public object LibraryServiceImpl { get; private set; }

        public ExchangeService Service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);


        //Change made here add Client Application Builder async to obtain OAuth Credentials
        public async void TokenAuthCreationAsync()
        {
            var cca = ConfidentialClientApplicationBuilder
              .Create("293daa97-99cc-4a31-bac1-a65571cbd08e")
              .WithClientSecret("z088Q~U1qwc9WLTLRx~QFjJFOAJLFbvDJF1PXbMB")
              .WithTenantId("b964694f-0cb9-4e9b-b605-51693b7011ef")
              .Build();

            var ewsScopes = new string[] { "https://outlook.office365.com/.default" };

            Microsoft.Identity.Client.AuthenticationResult authResult = null;
            try
            {
                authResult = cca.AcquireTokenForClient(ewsScopes).ExecuteAsync().Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

            try
            {
                var ewsClient = new ExchangeService();
                ewsClient.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");
                ewsClient.Credentials = new OAuthCredentials(authResult.AccessToken);
                ewsClient.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, "refresh@hosto.com");
                ewsClient.HttpHeaders.Add("X-AnchorMailbox", "refresh@hosto.com");

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        var url = "https://outlook.office365.com/EWS/Exchange.asmx";
                        var response = await client.GetStringAsync(url);

                        Console.WriteLine("response =>" + response);

                    }
                }                  
                    catch (HttpRequestException e)
                    {
                        // Handle HTTP request exceptions here.
                        Console.WriteLine($"Request exception: {e.Message}");
                    }
               
            }
            catch (Exception ex)
            {
                    Console.WriteLine("Error: " + ex);
            }
}
           

        private Exchange(string email, bool debug = true)
        {
            //Service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
            Service.TraceEnabled = debug;
            Service.TraceFlags = TraceFlags.All;
            // Accept Expired Certificates, If Otherwise Valid
            ServicePointManager.ServerCertificateValidationCallback += ValidateCertificate;
            // Set Email
            this._Email = email;
            // Set URL
            Service.Url = new Uri(@"https://outlook.office365.com/EWS/Exchange.asmx");
        }
        public Exchange(bool debug = false) : this(UserPrincipal.FindByIdentity(new PrincipalContext(ContextType.Domain), WindowsIdentity.GetCurrent().Name)?.EmailAddress, debug)
        {
            Service.UseDefaultCredentials = true;
        }
        public Exchange(string email, string password, bool debug = false) : this(email, debug)
        {
            try
            {
                Service.UseDefaultCredentials = false;
                this.Credentials = new NetworkCredential(email, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (Convert.ToDateTime(certificate.GetExpirationDateString()) < DateTime.Now
                && sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors
                && chain.ChainStatus.Count() == 1
                && chain.ChainStatus[0].Status == X509ChainStatusFlags.NotTimeValid
                )
                return true;
            else
                return sslPolicyErrors == SslPolicyErrors.None;
        }

        public EmailMessage GetEmailById(string UniqueID)
        {
            //change made here bm
            TokenAuthCreationAsync();
            return RunExchangeOperation(() => { return EmailMessage.Bind(this.Service, new ItemId(UniqueID)); });
        }

        public EmailMessage[] GetEmails(WellKnownFolderName folder, string SearchString = "")
        {
            ItemView view = new ItemView(int.MaxValue);
            view.PropertySet = new PropertySet(BasePropertySet.IdOnly, ItemSchema.Subject, ItemSchema.DateTimeSent, ItemSchema.HasAttachments);
            //change made here
            TokenAuthCreationAsync();
            return RunExchangeOperation(() => { return Service.FindItems(folder, new SearchFilter.ContainsSubstring(ItemSchema.Subject, SearchString), view); }).Items.OfType<EmailMessage>().ToArray();
        }

        public void SendEmail(string Subject, string Body, string[] Recipients, string[] CcRecipients, string[] Attachments, bool SaveCopy = false)
        {
            // Change made here
            var email = new EmailMessage(Service);
            //EmailMessage message = new EmailMessage(Service);
            //from EmailMessage. to email.
            email.Subject = Subject;
            email.Body = Body;

            if (Attachments != null)
            {


                foreach (string Recipient in Recipients)
                {
                     email.ToRecipients.Add(Recipient);
                    
                }
                if (CcRecipients != null)
                {
                    foreach (string Recipient in CcRecipients)
                    {
                         email.CcRecipients.Add(Recipient);
                        
                    }
                }
                if (SaveCopy)
                {
                    //change made here
                    TokenAuthCreationAsync();
                    RunExchangeOperation(() => { email.SendAndSaveCopy(); });
                    
                }
                else
                {
                    //change made here
                    TokenAuthCreationAsync();
                    RunExchangeOperation(() => { email.Send(); });
                    
                }
            }
        }

        public void SendEmail(string Subject, string Body, string[] Recipients, string[] CcRecipients = null, Attachment[] Attachments = null, bool SaveCopy = false)
        {
            // Change made here
            var email = new EmailMessage(Service);
            //EmailMessage message = new EmailMessage(Service);

            email.Subject = Subject;
            email.Body = Body;
            if (Attachments != null)
            {
                foreach (Attachment ATT in Attachments)
                {
                    email.Attachments.AddFileAttachment(ATT.Name, ATT.Bytes);//.Content); // STOPPED BEING ABLE TO USE STREAMS OF ANY KIND !!!
                }
            }
            foreach (string Recipient in Recipients)
            {
                email.ToRecipients.Add(Recipient);
            }
            if (CcRecipients != null)
            {
                foreach (string Recipient in CcRecipients)
                {
                    email.CcRecipients.Add(Recipient);
                }
            }
            if (SaveCopy)
            {
                //change made here
                TokenAuthCreationAsync();
                RunExchangeOperation(() => { email.SendAndSaveCopy(); });
            }
            else
            {
                // Change made here
                //RunExchangeOperation(() => { message.Send(); });
                TokenAuthCreationAsync();
                RunExchangeOperation(() => { email.Send(); });
            }
        }

        public void SetCredentials(string Username, string Password)
        {
            Service.UseDefaultCredentials = false;
            this.Credentials = new NetworkCredential(Username, Password);
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

                //change made here
                TokenAuthCreationAsync();
                RunExchangeOperation(() => { A.Save(SendInvitationsMode.SendOnlyToAll); });

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

                //change made here
                TokenAuthCreationAsync();
                RunExchangeOperation(() => { A.Save(SendInvitationsMode.SendOnlyToAll); });

                return Convert.FromBase64String(A.Id.UniqueId);
            }
            else
            {
                return null;
            }
        }

        public List<Appointment> FindAppointments(string SearchString, DateTime StartTime, DateTime EndTime)
        {
            //change made here
            TokenAuthCreationAsync();
            return RunExchangeOperation(() => { return Service.FindAppointments(WellKnownFolderName.Calendar, new CalendarView(StartTime, EndTime)).Where(el => el.Subject == SearchString).ToList(); });
        }

        public void CancelCalendarEvent(byte[] UniqueID, string CancellationMessage = "")
        {
            //change made here
            TokenAuthCreationAsync();
            RunExchangeOperation(() =>
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
            });
        }

        public PullSubscriber CreatePullEvents(string Watermark, List<FolderId> Folders, params EventType[] Events)
        {
            //change made here
            TokenAuthCreationAsync();
            return RunExchangeOperation(() => { return new PullSubscriber(this.Service, Folders, 120, Watermark, Events); });
        }

        private void RunExchangeOperation(System.Action operation)
        {
            if (string.IsNullOrEmpty(this.Service.Url?.AbsolutePath))
            {
                // Auto-Discover Exchange Server
                try
                {
                    Service.AutodiscoverUrl(this._Email, (siteUrl) => { return true; });
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, "The Autodiscover service couldn't be located."))
                    {
                        var credentials = OnUnAuthorizedException(ex);
                        if (credentials != null)
                        {
                            //Change Here
                            TokenAuthCreationAsync();
                            //this.Credentials = credentials;
                            RunExchangeOperation(operation);
                            return;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            try
            {
                operation();
            }
            catch (ServiceRequestException wex)
            {
                if (wex.Message.Contains("401"))
                {

                    var credentials = OnUnAuthorizedException(wex);
                    if (credentials != null)
                    {
                        //Change Here
                        TokenAuthCreationAsync();
                        //this.Credentials = credentials;
                        RunExchangeOperation(operation);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }
        private T RunExchangeOperation<T>(Func<T> operation)
        {
            if (string.IsNullOrEmpty(this.Service.Url?.AbsolutePath))
            {
                // Auto-Discover Exchange Server
                try
                {
                    Service.AutodiscoverUrl(this._Email, (siteUrl) => { return true; });
                }
                catch (Exception ex)
                {
                    if (string.Equals(ex.Message, "The Autodiscover service couldn't be located."))
                    {
                        var credentials = OnUnAuthorizedException(ex);
                        if (credentials != null)
                        {
                            //Change Here
                            TokenAuthCreationAsync();
                            //this.Credentials = credentials;
                            return RunExchangeOperation(operation);
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }

            try
            {
                return operation();
            }
            catch (ServiceRequestException wex)
            {
                if (wex.Message.Contains("401"))
                {
                    var credentials = OnUnAuthorizedException(wex);
                    if (credentials != null)
                    {
                        //Change Here
                        TokenAuthCreationAsync();
                        //this.Credentials = credentials;
                        return RunExchangeOperation(operation);
                    }
                    else
                    {
                        throw;
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        public void Dispose()
        {
            this.Service = null;
        }

        public class Attachment
        {
            public string Name { get; set; }
            //public System.IO.Stream Content { get; set; }
            public byte[] Bytes { get; set; }

            public Attachment(string Name, System.IO.Stream Content)
            {
                this.Name = Name;
                this.Bytes = new byte[Content.Length];
                Content.Read(this.Bytes, 0, (int)Content.Length);
            }
        }
    }
}

