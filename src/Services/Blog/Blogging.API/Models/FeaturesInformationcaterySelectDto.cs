using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogging.API.Models
{
    public class FeaturesInformationcaterySelectDto
    {
        public ValueId<string> featuresCategory { get; set; }
        public List<FeaturesInformationSelectDTO> featuresInformationSelectDTOs { get; set; }
    }
}
