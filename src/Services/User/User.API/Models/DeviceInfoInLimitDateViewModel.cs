using System;

namespace User.API.Models
{
    public class DeviceInfoInLimitDateDTO
    {
        public string Market { get; set; }
        public DateTime CreateDate { get; set; }

    }

    public class DeviceInfoInLimitDateViewModel
    {

        public int googlePlay { get; set; }
        public int googlePlayPurchase { get; set; }
        public int googlePlayProfileCompleted { get; set; }

        public int web { get; set; }
        public int webPurchase { get; set; }
        public int webProfileCompleted { get; set; }

        public int myket { get; set; }
        public int myketPurchase { get; set; }
        public int myketProfileCompleted { get; set; }

        public int appStore { get; set; }
        public int appStorePurchase { get; set; }
        public int appStoreProfileCompleted { get; set; }

        public int cafeBazar { get; set; }
        public int cafeBazarPurchase { get; set; }
        public int cafeBazarProfileCompleted { get; set; }

        public int AllProfileCompleted { get; set; }
        public int AllPurchase { get; set; }
        public int AllSuccessPurchase { get; set; }

        public OrderAmountDTO OrderAmountDTO { get; set; }
    }
}
