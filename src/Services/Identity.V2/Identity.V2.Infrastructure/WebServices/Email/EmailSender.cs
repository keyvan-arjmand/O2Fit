using System.Net;
using System.Net.Mail;
using System.Text;

namespace Identity.V2.Infrastructure.WebServices.Email;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SenderAsync(string message, string subject, string emailSender, string? languageName)
    {
        languageName ??= "English";


        try
        {
            var hostAddress = _configuration["EmailSettings:Host"];
            var formEmail = _configuration["EmailSettings:MailFrom"];
            var password = _configuration["EmailSettings:Password"];
            var port = _configuration["EmailSettings:Port"];
            bool ssl = bool.Parse(_configuration["EmailSettings:Ssl"]!);

            MailMessage mailMessage = new MailMessage();

            if (formEmail != null)
            {
                mailMessage.From = new MailAddress(formEmail);
                mailMessage.Subject = subject;

                mailMessage.To.Add(new MailAddress(emailSender));
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;

                switch (languageName)
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
                networkCredential.UserName = formEmail;
                networkCredential.Password = password;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = hostAddress;
                smtp.Port = Convert.ToInt32(port);
                smtp.EnableSsl = ssl;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;

                await smtp.SendMailAsync(mailMessage).ConfigureAwait(false);
            }


            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}