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

        /*
         * default class constructor
         *
         * @author: Josué Quirós
         */
        public OrdersTransactions()
        {

        }

        /*
         * main class constructor
         *
         * @author: Josué Quirós
         *
         * @param otId: order transaction id number
         * @param oId: order id number
         * @param tId: Transaction id number
         */
        public OrdersTransactions(int otId, int oId, int tId)
        {
            OrderTransactionsId = otId;
            OrderId = oId;
            TransactionId = tId;
        }
    }
}
