using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Identity.Domain.Entities.Country
{
    public class Country : BaseEntity
    {
        public int NameId { get; set; }
        public string FlagIcon { get; set; }
        public ICollection<City> Cities { get; set; }

        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
    }
}
