using System;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Data.Contracts;
using FoodStuff.Domain.Entities.Translation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoodStuff.Service.v2.Command.Translations
{
    public class UpdateNewTranslationCommandHandler : IRequestHandler<UpdateNewTranslationCommand, Translation>, IScopedDependency
    {
        private readonly ITranslationRepository _repository;

        public UpdateNewTranslationCommandHandler(ITranslationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Translation> Handle(UpdateNewTranslationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var translation =
                    await _repository.Table.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (translation == null)
                    throw new AppException(ApiResultStatusCode.ServerError, "Update translation command");

                translation.Arabic = request.Arabic;
                translation.English = request.English;
                translation.Persian = request.Persian;
                await _repository.UpdateAsync(translation, cancellationToken);
                return translation;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}