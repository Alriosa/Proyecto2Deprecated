using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class OrdersProducts : BaseEntity{
        public int OrdersProductsId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double UnitPriceNoTax { get; set; }
        public int OrderId { get; set; }

        public OrdersProducts() {
        }

        public OrdersProducts(int ordersProductsId, int productId, int quantity, double unitPriceNoTax, int orderId) {
            OrdersProductsId = ordersProductsId;
            ProductId = productId;
            Quantity = quantity;
            UnitPriceNoTax = unitPriceNoTax;
            OrderId = orderId;
        }
    }
}
