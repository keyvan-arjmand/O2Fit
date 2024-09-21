using MediatR;
using Ordering.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.v1.Command
{
    public class UpdateTranslationCommand : IRequest<Translation>
    {
        public Translation Translation { get; set; }
    }
}
