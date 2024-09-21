using System;
using MediatR;

namespace Service.v2.Command
{
    public class AddConfirmationCodeCommand : IRequest<Unit>
    {
        public string Username { get; set; }
        public string ConfirmCode { get; set; }
        public DateTime ConfirmCodeExpireTime { get; set; }
    }
}