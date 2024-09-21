using System;
using System.Collections.Generic;
using System.Text;

namespace FoodStuff.Domain.Entities.Food
{
    public class FastOffline
    {
        public string UserId { get; set; }
        public string LocalId { get; set; }
        public string Time { get; set; }

        #region Cache Attributes
        public double Score { get; set; } = 0;
        public string Tag { get; set; }
        #endregion
    }
}
