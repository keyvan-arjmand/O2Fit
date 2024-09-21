using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Common;


public abstract class BaseEntity
{
    [Key] public long Id { get; set; }
    public bool IsDelete { get; set; }= false;
}