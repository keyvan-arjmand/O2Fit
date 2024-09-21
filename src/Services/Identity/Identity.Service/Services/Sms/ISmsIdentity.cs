using System.Threading.Tasks;

namespace Identity.Service.Services.Sms
{
    public interface ISmsIdentity
    {
        Task<bool> VerificationCodeAsync(string phone, string code);
        Task<bool> ChangePasswordAsync(string phone, string code);
    }
}