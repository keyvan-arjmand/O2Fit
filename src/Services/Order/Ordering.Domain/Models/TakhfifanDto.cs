using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Domain.Models
{
   public class TakhfifanDto
   {
      public string token { get; set; }  //"transaction_id": "#3290",
      public string transaction_id { get; set; }  //"transaction_id": "#3290",
      public string revenue { get; set; }//"revenue": 19.99,
      public string shipping { get; set; }//"shipping": 3,
      public string tax { get; set; }//"tax": 1.66,
      public string discount { get; set; }//"discount": 0,
      public string new_customer { get; set; }//"new_customer": true,
      public string affiliation { get; set; }// “affiliation”: ”takhfifan”,
      public List<item> items { get; set; }

   }

   public class item
   {
      public string sku { get; set; }    //"sku": "sku1-HD",
      public string category { get; set; }        //"category": "movies",
      public string price { get; set; }  //"price": 5000,
      public string product_id { get; set; } // “product_id”: “id-1010”,
      public string quantity { get; set; }     //"quantity": 3,
      public string name { get; set; }//“name”: “جنگ ستارگان”
   }
}
