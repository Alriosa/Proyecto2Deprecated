using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class OrdersProductsInventory : BaseEntity{
        public int OrdersProductsInventoryId { get; set; }
        public int InventoryId { get; set; }
        public int OrdersProductsId { get; set; }

        public OrdersProductsInventory() {
        }

        public OrdersProductsInventory(int ordersProductsInventoryId, int inventoryId, int ordersProductId) {
            OrdersProductsInventoryId = ordersProductsInventoryId;
            InventoryId = inventoryId;
            OrdersProductsId = ordersProductId;
        }
    }
}
