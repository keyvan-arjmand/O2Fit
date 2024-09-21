using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Service.v1.Command
{
    public class DeleteAdvertiseCountryCommand : IRequest
    {
        public List<string> Keys { get; set; }
    }
}
