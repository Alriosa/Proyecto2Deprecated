using System.Collections.Generic;

namespace EntitiesPOJO {
    public class OrderPreview{
        public Orders Order { get; set; }
        public List<OrdersProducts> Products { get; set; }

        public OrderPreview() {
        }

        public OrderPreview(Orders order, List<OrdersProducts> products) {
            Order = order;
            Products = products;
        }
    }
}
