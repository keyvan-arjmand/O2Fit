using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Identity.Service.Services.Sms
{
    public class SmsIdentity : ISmsIdentity, IScopedDependency
    {
        private readonly SiteSettings _siteSetting;
        public SmsIdentity(IOptionsSnapshot<SiteSettings> settings)
        {
            _siteSetting = settings.Value;
        }

        public async Task<bool> VerificationCodeAsync(string phone, string code)
        {
            SmsIrRestfulNetCore.Token tokenInstance = new SmsIrRestfulNetCore.Token();

            string _SecurityCode = _siteSetting.SmsSettings.SecurityCode;
            string _ApiKey = _siteSetting.SmsSettings.ApiKey;

            var token = tokenInstance.GetToken(_ApiKey, _SecurityCode);

            var ultraFastSend = new SmsIrRestfulNetCore.UltraFastSend()
            {
                Mobile = Convert.ToInt64(phone),
                TemplateId = 41714,
                ParameterArray = new List<SmsIrRestfulNetCore.UltraFastParameters>()
                {
                   new SmsIrRestfulNetCore.UltraFastParameters()
                   {
                        Parameter = "VerificationCode" , ParameterValue = code
                   }
                }.ToArray()
            };

            SmsIrRestfulNetCore.UltraFastSendRespone ultraFastSendResponse = new SmsIrRestfulNetCore.UltraFast().Send(token, ultraFastSend);

            if (ultraFastSendResponse.IsSuccessful)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ChangePasswordAsync(string phone, string code)
        {
            SmsIrRestfulNetCore.Token tokenInstance = new SmsIrRestfulNetCore.Token();

            string _SecurityCode = _siteSetting.SmsSettings.SecurityCode;
            string _ApiKey = _siteSetting.SmsSettings.ApiKey;

            var token = tokenInstance.GetToken(_ApiKey, _SecurityCode);

            var ultraFastSend = new SmsIrRestfulNetCore.UltraFastSend()
            {
                Mobile = Convert.ToInt64(phone),
                TemplateId = 16879,
                ParameterArray = new List<SmsIrRestfulNetCore.UltraFastParameters>()
            {
            new SmsIrRestfulNetCore.UltraFastParameters()
            {
                Parameter = "VerificationCode" , ParameterValue = code
            }}.ToArray()
            };

            SmsIrRestfulNetCore.UltraFastSendRespone ultraFastSendRespone = new SmsIrRestfulNetCore.UltraFast().Send(token, ultraFastSend);

            if (ultraFastSendRespone.IsSuccessful)
            {
                return true;
            }
            return false;
        }
    }
}
