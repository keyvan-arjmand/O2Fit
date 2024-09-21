using Identity.Domain.Entities.Translation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.v1.Query
{
    public class GetTranslationFullQuery :IRequest<List<Translation>>
    {
        public List<int> Ids { get; set; }
    }
}
