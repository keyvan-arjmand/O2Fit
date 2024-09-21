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
    public class DeleteNoteByIdCommandHandler: IRequestHandler<DeleteNoteByIdCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Note> _repository;

        public DeleteNoteByIdCommandHandler(IRepository<Note> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteNoteByIdCommand request, CancellationToken cancellationToken)
        {
            var note = await _repository.Table.FirstOrDefaultAsync(x => x.AppId == request.AppId, cancellationToken);
            if (note == null)
                throw new AppException(ApiResultStatusCode.NotFound);

            await _repository.DeleteAsync(note, cancellationToken);

            return Unit.Value;
        }
    }
}