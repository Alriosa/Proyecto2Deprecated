using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class PaymentMethod : BaseEntity
    {
        public int PaymentMethodId { get; set; }
        public string PaymentTypeCode { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public PaymentMethod()
        {

        }

        public PaymentMethod(int pmId, string pTypeCode, string val, int uId, bool isActive)
        {
            PaymentMethodId = pmId;
            PaymentTypeCode = pTypeCode;
            Value = val;
            UserId = uId;
            IsActive = isActive;
        }
    }
}
