using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Data.Contracts
{
    public interface IEmailSender
    {
        Task<bool> SenderAsync(string message, string subject, string emailSender , string LanguageName);
    }
}
