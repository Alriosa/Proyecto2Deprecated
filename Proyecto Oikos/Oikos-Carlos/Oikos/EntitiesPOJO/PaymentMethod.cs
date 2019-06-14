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

        /*
         ** default class constructor
         *
         * @author: Josué Quirós        
         */
        public PaymentMethod()
        {

        }

        /*
         * main class constructor
         *
         * @author: Josué Quirós
         *
         * @param pmId: Payment method id number
         * @param pTypeCode: Payment method type code number
         * @param val: value of the payment method
         * @param uId: user id number linked to the payment method
         * @param isActive: status of payment method
         */
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
