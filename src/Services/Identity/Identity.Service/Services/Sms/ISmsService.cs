using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Identity.Service.Services.Sms
{
    public interface ISmsService : IScopedDependency
    {
        public Task<bool> SendSmsNewVersionSmsIr(string phone, string code);
        public Task<bool> SendSmsRegisterNutritionist(string phone);
        public int Balance();
    }
}
