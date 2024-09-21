using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Advertising.Service.v1.Command
{
    public class UpdateAdvertiseByIdCommand :IRequest
    {
        public int Id { get; set; }
        public bool Click { get; set; }
        public bool View { get; set; }
        public bool Hint { get; set; }
    }
}
