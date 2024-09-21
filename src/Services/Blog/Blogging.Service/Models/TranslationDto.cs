using Blogging.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogging.API.Models
{
    public class TranslationDto
    {
        public string Persian { get; set; }
        public string English { get; set; }
        public string Arabic { get; set; }
    }
}
