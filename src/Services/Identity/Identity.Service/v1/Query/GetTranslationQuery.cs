using Data.Repositories;
using Identity.Domain.Entities.Translation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.v1.Query
{
    public class GetTranslationQuery : IRequest<List<Translation>>
    {
        public List<int> Ids { get; set; }
        public string Language { get; set; }
    }
}
