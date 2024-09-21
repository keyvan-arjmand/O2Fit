using System;
using System.Collections.Generic;
using System.Text;
using Advertising.Domain.Entities.Translation;
using MediatR;

namespace Service.v1.Command
{
    public class CraeteTranslationCommand : IRequest<Translation>
    {
        public Translation Translation { get; set; }
    }
}
