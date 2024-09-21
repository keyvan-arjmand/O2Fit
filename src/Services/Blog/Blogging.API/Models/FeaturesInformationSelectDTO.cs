using Blogging.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace Blogging.API.Models
{
    public class FeaturesInformationSelectDTO 
    {
        public int Id { get; set; }
        public ValueId<string> Title { get; set; }

        public ValueId<string> SubTitle { get; set; }

        public ValueId<string> Description { get; set; }

        public ValueId<string> Image { get; set; }

        public ValueId<string> Video { get; set; }

        public string Icon { get; set; }

   
    }
}
