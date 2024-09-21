using Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities.Discount
{
    public class Discount : BaseEntity
    {
        public string Code { get; set; }
        public int Percent { get; set; }
        public Nullable<int> UsableCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDateTime { get; set; }
        public Nullable<int> UserId { get; set; }
        public bool IsActive { get; set; }
        public int NameId { get; set; }
        [ForeignKey(nameof(NameId))]
        public Translation.Translation Translation { get; set; }
    }
}
