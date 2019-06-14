using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class UsersShippingProviders:BaseEntity {
    public int UsersShippingProvidersId { get; set; }
    public int UserId { get; set; }
    public int ShippingProviderId { get; set; }

    public UsersShippingProviders(){

    }

    public UsersShippingProviders(int usersStoresId, int userId, int shippingProviderId){
        this.UsersShippingProvidersId = usersStoresId;
        this.UserId = userId;
        this.ShippingProviderId = shippingProviderId;
    }
    }
}
