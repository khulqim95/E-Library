//using MailKit.Net.Smtp;
//using MailKit.Security;
//using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace PortalPMO.Component
{
    public class MailUtil
    {
        public const string ADMIN_MAILADDRESS = "admin@gmail.com";
        public const string ADMIN_MAILADDRESS_ALIAS = "Admin";
        public const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        //public static void SendEmail(String From, String To, String Subject, String BodyMessage)
        //{
        //    try
        //    {
        //        if (To != null)
        //        {
        //            if (To.Trim() != "")
        //            {
        //                ThreadPool.QueueUserWorkItem(t =>
        //                {
        //                    MailMessage Message = new MailMessage();
        //                    Message.From = new MailAddress(From, GetConfig.AppSetting["AppSettings:GlobalSettings:mAilAddressAlias"]);
        //                    foreach (string address in To.Split(';'))
        //                    {
        //                        try
        //                        {
        //                            if (address != null && address != "")
        //                            {
        //                                if (Regex.IsMatch(address, MatchEmailPattern))
        //                                {
        //                                    Message.To.Add(address);
        //                                }
        //                            }
        //                        }
        //                        catch
        //                        {
        //                        }
        //                    }

        //                    Message.Subject = Subject;
        //                    Message.Body = BodyMessage;
        //                    Message.IsBodyHtml = true;

        //                    try
        //                    {
        //                        SmtpClient SmtpClient = new SmtpClient();
        //                        SmtpClient.SendCompleted += (s, e) =>
        //                        {
        //                            SmtpClient.Dispose();
        //                            Message.Dispose();
        //                        };
        //                        SmtpClient.SendAsync(Message, DateTime.Now);
        //                    }
        //                    catch
        //                    {
        //                    }
        //                });
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        //public static string SendMailGmail(string recipient, string subject, string htmlBody, string name)
        //{
        //    string FromAddress = "no-reply@qantor.id";
        //    string FromAdressTitle = "Kode validasi Pendaftaran Anggota";
        //    //To Address  
        //    string ToAddress = recipient;
        //    string ToAdressTitle = name;
        //    string Subject = subject;

        //    //Smtp Server  
        //    string SmtpServer = "smtp.gmail.com";
        //    //Smtp Port Number  
        //    int SmtpPortNumber = 587;

        //    var mimeMessage = new MimeMessage();
        //    mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
        //    mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
        //    mimeMessage.Subject = Subject;
        //    //mimeMessage.Body = new TextPart("plain")
        //    //{
        //    //    Text = BodyContent

        //    //};
        //    var bodyBuilder = new BodyBuilder();
        //    //bodyBuilder.HtmlBody = @"<b style='color:blue'>This is bold and this is <i>italic</i></b>";
        //    bodyBuilder.HtmlBody = @htmlBody;
        //    mimeMessage.Body = bodyBuilder.ToMessageBody();

        //    using (var client = new SmtpClient())
        //    {
        //        try
        //        {
        //            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
        //            //client.ConnectAsync(SmtpServer, SmtpPortNumber);
        //            client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);


        //            client.AuthenticationMechanisms.Remove("XOAUTH2");
        //            client.Authenticate("rajanyapoker0002@gmail.com", "h4nd1k4d3v1");
        //            // Note: only needed if the SMTP server requires authentication  
        //            // Error 5.5.1 Authentication  
        //            //client.AuthenticationMechanisms.Remove("NTLM");
        //            //await client.AuthenticateAsync(FromAddress, "");
        //            client.SendAsync(mimeMessage);
        //            client.DisconnectAsync(true);
        //            return "sukses";
        //        }
        //        catch (Exception ex)
        //        {
        //            return ex.ToString();
        //        }
        //    }
        //}


        public static string SendMail(string recipient, string subject, string message)
        {
            string Res = "";
            string _sender = "sistemrombis@gmail.com";
            string _password = "handika_rombis";

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;

            try
            {
                MailMessage mail = new MailMessage();
                //var mail = new MailMessage(_sender.Trim(), recipient.Trim());
                mail.From = new MailAddress("admin-rombis@qantor.id", "admin");
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                client.Send(mail);
                Res = "sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Res = ex.Message;

                throw ex;
            }
            return Res;
        }
    }
}
