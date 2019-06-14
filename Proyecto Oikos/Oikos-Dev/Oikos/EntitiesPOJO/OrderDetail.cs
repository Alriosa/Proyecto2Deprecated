using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class OrderDetail {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderDatetime { get; set; }
        public ShippingMethods ShippMethod { get; set; }
        public UserLocation Destination { get; set; }
        public List<OrdersProducts> Products { get; set; }
        public OrdersStatus Status { get; set; }

        public OrderDetail() {
            Products = new List<OrdersProducts>();
        }

        public OrderDetail(string userName, string userEmail, ShippingMethods shippMethod, DateTime orderDatetime,
            UserLocation destination, List<OrdersProducts> products, OrdersStatus status) {
            UserName = userName;
            UserEmail = userEmail;
            ShippMethod = shippMethod;
            OrderDatetime = orderDatetime;
            Destination = destination;
            Products = products;
            Status = status;
        }
    }
}