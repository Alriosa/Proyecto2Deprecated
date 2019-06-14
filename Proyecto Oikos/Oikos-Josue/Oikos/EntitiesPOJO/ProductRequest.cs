using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class ProductRequest : BaseEntity
    {
        public int ProductRequestId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime RequestDatetime { get; set; }
        public DateTime ExpirationDatetime { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }

        public ProductRequest()
        {

        }

        public ProductRequest(int prodReqId, int userId, string desc, DateTime reqDate, DateTime expDate, bool isActive, int qty)
        {
            ProductRequestId = prodReqId;
            UserId = userId;
            Description = desc;
            RequestDatetime = reqDate;
            ExpirationDatetime = expDate;
            IsActive = isActive;
            Quantity = qty;
        }
    }
}
