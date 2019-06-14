using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class OrdersTransactions : BaseEntity
    {
        public int OrderTransactionsId { get; set; }
        public int OrderId { get; set; }
        public int TransactionId { get; set; }

        public OrdersTransactions()
        {

        }

        public OrdersTransactions(int otId, int oId, int tId)
        {
            OrderTransactionsId = otId;
            OrderId = oId;
            TransactionId = tId;
        }
    }
}
