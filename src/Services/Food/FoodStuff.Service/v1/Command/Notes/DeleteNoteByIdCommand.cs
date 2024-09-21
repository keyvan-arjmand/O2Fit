using MediatR;

namespace FoodStuff.Service.v1.Command.Notes
{
    public class DeleteNoteByIdCommand : IRequest<Unit>
    {
        public string AppId { get; set; }
    }
}