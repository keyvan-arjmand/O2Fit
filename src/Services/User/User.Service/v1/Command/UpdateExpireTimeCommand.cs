using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace User.Service.v1.Command
{
    public class UpdateExpireTimeCommand : IRequest
    {
        public int UserId { get; set; }
        public string Time { get; set; }
        public string DietTime { get; set; }
        public string Tid { get; set; }
    }
}
