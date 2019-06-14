using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class Coupon : BaseEntity
    {
        public int CouponId { get; set; }
        public string MakerCode { get; set; }
        public string ValueTypeCode { get; set; }
        public double Value { get; set; }
        public int MadeBy { get; set; }

        /*
         * default class constructor
         *
         * @author Josué Quirós
         */
        public Coupon()
        {

        }

        /*
         * main class constructor
         *
         * @author Josué Quirós
         *
         * @param cId: coupon id number
         * @param mCode: coupons maker code
         * @param vtypeCode: value type of coupon
         * @param value: amount covered by coupon
         * @madeBy: maker id number
         */
        public Coupon(int cId, string mCode, string vtypeCode, double value, int madeBy)
        {
            CouponId = cId;
            MakerCode = mCode;
            ValueTypeCode = vtypeCode;
            Value = value;
            MadeBy = madeBy;
        }
    }
}
