using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Entities.Translation;

namespace Service.v1.Command
{
    public class UpdateTranslationCommand : IRequest<Translation>
    {
        public Translation Translation { get; set; }
    }
}
