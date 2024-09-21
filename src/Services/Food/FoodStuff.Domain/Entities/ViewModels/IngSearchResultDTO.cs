using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.ViewModels
{
    public class IngSearchResultDTO
    {
        public string NameTranslate { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
    }

    public class IngSearchQueryDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
