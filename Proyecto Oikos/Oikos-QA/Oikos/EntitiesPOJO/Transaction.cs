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
        public int OriginalInternalAmount { get; set; }
        public int DestinationInternalAccountId { get; set; }
        public int PaymentMethodId { get; set; }

        public Transaction()
        {

        }

        public Transaction(int tId, 
                           string tTypeCode, 
                           string dtl, 
                           double amnt, 
                           int originalIntAmount,
                           int destInternalAcctId,  
                           int payMethodId)
        {
            TransactionId = tId;
            TransactionTypeCode = tTypeCode;
            Detail = dtl;
            Amount = amnt;
            OriginalInternalAmount = originalIntAmount;
            DestinationInternalAccountId = destInternalAcctId;
            PaymentMethodId = payMethodId;
        }
    }
}
