using FoodStuff.Domain.Entities.Food;
using FoodStuff.Domain.Entities.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Api;

namespace FoodStuff.API.Models
{
    public class BrandSelectDTO:BaseDto<BrandDTO, Brand,int>
    {
       public string Name { get; set; }
       public string LogoUri { get; set; }
       public string Address { get; set; }
    }
    public class BrandSelectAdminDTO : BaseDto<BrandDTO, Brand, int>
    {
        public Translation Name { get; set; }
        public string LogoUri { get; set; }
        public string Address { get; set; }
    }
}
