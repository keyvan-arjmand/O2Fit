namespace Identity.V2.Infrastructure.WebServices.Sms;

public interface ISmsService
{
    public Task<bool> SendSmsNewVersionSmsIrAsync(string phone, string code);
    public Task<bool> SendSmsRegisterNutritionistAsync(string phone);
    public int Balance();
}