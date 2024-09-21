namespace Notification.Application.Common.Interfaces.Services;

public interface ISmsService
{
    public Task<bool> SendSmsNewVersionSmsIrAsync(string phone, string code);
    public Task<bool> SendSmsRegisterNutritionistAsync(string phone);
    public int Balance();
    public Task<bool> SendSmsAsync(string phone, string title, string message);
}