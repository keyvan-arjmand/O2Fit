using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WorkoutTracker.Data.Repositories;

namespace Service.v1.Query
{
    public class GetTranslationQuery : IRequest<List<SelectOption<int>>>
    {
        public List<int> Ids { get; set; }
        public string Language { get; set; }
    }
}
