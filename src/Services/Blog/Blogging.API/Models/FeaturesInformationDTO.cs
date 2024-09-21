using Blogging.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Blogging.API.Models
{
    public class FeaturesInformationDTO
    {
        public TranslationDto Title { get; set; }

        public TranslationDto SubTitle { get; set; }

        public TranslationDto Description { get; set; }

        public TranslationDto Image { get; set; }

        public TranslationDto Video { get; set; }

        public string Icon { get; set; }

        public int featuresCategory { get; set; }
    }
}
