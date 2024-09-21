using Common;
using Identity.Data.Contracts;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Data.Repositories
{
    public class EmailSender : IEmailSender, IScopedDependency
    {
        private readonly SiteSettings _settings;

        public EmailSender(IOptionsSnapshot<SiteSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SenderAsync(string message, string subject, string emailSender, string LanguageName)
        {
            LanguageName = LanguageName == null ? "English" : LanguageName;

            bool status = false;

            try
            {
                string HostAddress = _settings.EmailSettings.Host;
                string FormEmail = _settings.EmailSettings.MailFrom;
                string Password = _settings.EmailSettings.Password;
                string Port = _settings.EmailSettings.Port;
                bool Ssl = _settings.EmailSettings.Ssl;

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(FormEmail);
                mailMessage.Subject = subject;

                mailMessage.To.Add(new MailAddress(emailSender));
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;

                switch (LanguageName)
                {
                    case "Persian":
                        {
                            mailMessage.Body = "با سلام و احترام<br />" +
                             $"کاربر گرامی کد فعالسازی شما : <b>{message}</b><br/>" +
                             "با تشکر گروه اکسیژن فیت <br/>" +
                             "<b>www.o2fitt.com</b>";
                            break;
                        }
                    case "English":
                        {
                            mailMessage.Body = "Dear user <br/>" +
                             $"your activation code is : <b>{message}</b><br/>" +
                             $"Sincerely yours, O2Fit Group<br/>" +
                             "<b>www.o2fitt.com</b>";
                            break;
                        }
                    case "Arabic":
                        {
                            mailMessage.Body = "عزيزي المستخدم <br/>" +
                           $"رمز التفعيل الخاص بك هو : <b>{message}</b><br/>" +
                           "مع خالص التقدير لك ، O2Fit Group <br/>" +
                           "<b>www.o2fitt.com</b>";
                            break;
                        }
                    default:
                        break;
                }

                NetworkCredential networkCredential = new NetworkCredential();
                networkCredential.UserName = FormEmail;
                networkCredential.Password = Password;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = HostAddress;
                smtp.Port = Convert.ToInt32(Port);
                smtp.EnableSsl = Ssl;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;

                await smtp.SendMailAsync(mailMessage);

                status = true;

                return status;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
