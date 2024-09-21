using Blogging.Domain.Entities.Translation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.v1.Query
{
    public class GetTranslationByIdQuery : IRequest<Translation>
    {
        public int Id { get; set; }
    }
}
