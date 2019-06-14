using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class Inventory:BaseEntity{
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        public decimal PriceBought { get; set; }
        public DateTime PurchaseDatetime { get; set; }
        public int SerialNumber { get; set; }
        public bool IsSold { get; set; }

        public Inventory() {
        }

        public Inventory(int inventoryId, int productId, decimal priceBought, DateTime purchaseDatetime, int serialNumber, bool isSold) {
            InventoryId=inventoryId;
            ProductId=productId;
            PriceBought=priceBought;
            PurchaseDatetime=purchaseDatetime;
            SerialNumber=serialNumber;
            IsSold=isSold;
        }
        public Inventory(int productId, decimal priceBought, DateTime purchaseDatetime, int serialNumber, bool isSold) {
            ProductId=productId;
            PriceBought=priceBought;
            PurchaseDatetime=purchaseDatetime;
            SerialNumber=serialNumber;
            IsSold=isSold;
        }
    }
}
