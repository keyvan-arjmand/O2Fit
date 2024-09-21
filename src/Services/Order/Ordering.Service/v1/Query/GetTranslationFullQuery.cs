using MediatR;
using Ordering.Domain.Entities.Translation;
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
