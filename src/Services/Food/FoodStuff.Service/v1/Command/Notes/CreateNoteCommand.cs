using System.Collections.Generic;
using Amazon.Runtime.Internal;
using MediatR;

namespace FoodStuff.Service.v1.Command.Notes
{
    public class CreateNoteCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public string _id { get; set; }
    }
}