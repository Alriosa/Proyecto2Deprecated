using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO
{
    public class InternalAccount : BaseEntity
    {
        public int InternalAccountId { get; set; }
        public int UserId { get; set; }
        public string DestinationAccount { get; set; }
        public double Balance { get; set; }


        /*
         ** default class constructor
         *
         * @author: Josué Quirós        
         */
        public InternalAccount()
        {

        }


        /*
         * main class constructor
         *
         * @author: Josué Quirós
         *
         * @param intAcctId: Internal account id number
         * @param userId: User's Id number
         * @param destAcct: value of the destination account
         * @param balance: account's new balance
         */
        public InternalAccount(int intAcctId, int userId, string destAcct, double balance)
        {
            InternalAccountId = intAcctId;
            UserId = userId;
            DestinationAccount = destAcct;
            Balance = balance;
        }
    }
}
