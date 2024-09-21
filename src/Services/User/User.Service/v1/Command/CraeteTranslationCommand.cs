using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using User.Domain.Entities.Translation;

namespace Service.v1.Command
{
    public class CraeteTranslationCommand : IRequest<Translation>
    {
        public Translation Translation { get; set; }
    }
}
