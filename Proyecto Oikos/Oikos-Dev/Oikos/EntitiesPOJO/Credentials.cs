using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class Credentials:User{
        public List<UserRoleView> RolesViewsList{ get; set;}
        public int StoreId { get; set;}
        public int ShippingProviderId { get; set;}
        

        public Credentials(int userId, string name, string email, string statusCode, string currencyCode){
            this.UserId = userId;
            this.Name = name;
            this.Email = email;
            this.UserStatusCode = statusCode;
            this.CurrencyCode = currencyCode;
        }
    }
}
