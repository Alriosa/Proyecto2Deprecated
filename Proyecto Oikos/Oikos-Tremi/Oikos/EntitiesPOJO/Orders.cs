using System;

namespace EntitiesPOJO {
    public class Orders : BaseEntity {
        public int OrderId { get; set; }
        public int ShippingMethodId { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDatetime { get; set; }
        public int Destination { get; set; }

        public Orders() {
        }

        public Orders(int orderId, int shippingMethodId, int storeId, int userId, DateTime orderDatetime,
            int destination) {
            OrderId = orderId;
            ShippingMethodId = shippingMethodId;
            StoreId = storeId;
            UserId = userId;
            OrderDatetime = orderDatetime;
            Destination = destination;
        }
    }
}