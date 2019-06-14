using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }
        public string TransactionTypeCode { get; set; }
        public string Detail { get; set; }
        public double Amount { get; set; }
        public int OriginInternalAccountId { get; set; }
        public int DestinationInternalAccountId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime TransactionDatetime { get; set; }


        /*
         * Default constructor
         *
         * @author: Josué Quirós
         */
        public Transaction()
        {

        }

        /*
         * Class constructor
         *
         * @author: Josué Quirós
         *
         * @param tId: the transaction id number
         * @param tTypeCode: transaction type code number
         * @param dtl: transaction details
         * @param amnt: transaction amount
         * @param originalIntAmount: the original amount in the account
         * @param destInternalAcctId: the destination account id number
         * @param payMethodId: the payment method id number
         */
        public Transaction(int tId, 
                           string tTypeCode, 
                           string dtl, 
                           double amnt, 
                           int originIntAccountId,
                           int destInternalAcctId,  
                           int payMethodId,
                           DateTime date)
        {
            TransactionId = tId;
            TransactionTypeCode = tTypeCode;
            Detail = dtl;
            Amount = amnt;
            OriginInternalAccountId = originIntAccountId;
            DestinationInternalAccountId = destInternalAcctId;
            PaymentMethodId = payMethodId;
            TransactionDatetime = date;
        }
    }
}
