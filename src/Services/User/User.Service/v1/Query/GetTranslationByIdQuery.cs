using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.Entities.Translation;

namespace Service.v1.Query
{
    public class GetTranslationByIdQuery : IRequest<Translation>
    {
        public int Id { get; set; }
    }
}
