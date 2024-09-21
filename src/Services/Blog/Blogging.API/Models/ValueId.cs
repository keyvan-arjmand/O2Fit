using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogging.API.Models
{
    public class ValueId<TValue>
    {
        public int Id { get; set; }
        public TValue Value { get; set; }
    }
}
