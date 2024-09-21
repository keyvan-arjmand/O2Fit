using System;
using System.Collections.Generic;
using System.Text;
using Blogging.Domain.Common;

namespace Blogging.Domain.Entities.Translation
{
    public class Translation : BaseEntity<int>
    {
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
    }
}
