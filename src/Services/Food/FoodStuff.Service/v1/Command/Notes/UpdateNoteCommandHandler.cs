using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v1.Command.Notes
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Note> _repository;

        public UpdateNoteCommandHandler(IRepository<Note> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _repository.Table.FirstOrDefaultAsync(x=>x.AppId == request._id, cancellationToken);
            if (note == null)
                throw new AppException(ApiResultStatusCode.NotFound);

            note.Text = request.Text;
            
            await _repository.UpdateAsync(note, cancellationToken);

            return Unit.Value;
        }
    }
}