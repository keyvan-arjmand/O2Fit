using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts;
using FoodStuff.Domain.Entities.Food;
using MediatR;

namespace FoodStuff.Service.v1.Command.Notes
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Unit>, IScopedDependency
    {
        private readonly IRepository<Note> _repository;

        public CreateNoteCommandHandler(IRepository<Note> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                Text = request.Text,
                InsertDate = DateTime.Now,
                UserId = request.UserId,
                AppId = request._id
            };

            await _repository.AddAsync(note, cancellationToken);

            return Unit.Value;
        }
    }
}