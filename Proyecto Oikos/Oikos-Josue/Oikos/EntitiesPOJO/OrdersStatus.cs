using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class OrdersStatus : BaseEntity{
        public int OrdersStatusId { get; set; }
        public int OrderId { get; set; }
        public int StatusCode { get; set; }
        public DateTime StatusChangeDatetime { get; set; }
        public bool IsActive { get; set; }

        public OrdersStatus() {
        }

        public OrdersStatus(int ordersStatusId, int orderId, int statusCode, DateTime statusChangeDatetime,bool isActive) {
            OrdersStatusId = ordersStatusId;
            OrderId = orderId;
            StatusCode = statusCode;
            StatusChangeDatetime = statusChangeDatetime;
            IsActive = isActive;
        }
    }
}
