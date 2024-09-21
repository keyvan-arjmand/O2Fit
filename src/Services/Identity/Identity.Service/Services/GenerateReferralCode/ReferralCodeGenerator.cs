using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Identity.Service.Services.GenerateReferralCode
{
    internal class ReferralCodeGenerator : IReferralCodeGenerator, ITransientDependency
    {
        public string Generate()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var referralCode = new string(
                Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

            return referralCode;
        }
       
    }
}
