using MediatR;

namespace FoodStuff.Service.v1.Command.Notes
{
    public class UpdateNoteCommand : IRequest<Unit>
    {
        public string _id { get; set; }
        public string Text { get; set; }
    }
}