namespace Identity.V2.Infrastructure.WebServices.Email;

public interface IEmailSender
{ 
    Task<bool> SenderAsync(string message, string subject, string emailSender , string? languageName);

}