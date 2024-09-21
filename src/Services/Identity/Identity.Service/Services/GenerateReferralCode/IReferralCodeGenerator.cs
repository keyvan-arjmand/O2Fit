using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Service.Services.GenerateReferralCode
{
    public interface IReferralCodeGenerator
    {
        public string Generate();
    }
}
