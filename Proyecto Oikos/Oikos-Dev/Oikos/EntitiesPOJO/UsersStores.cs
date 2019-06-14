using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class UsersStores:BaseEntity{
        public int UsersStoresId { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }

        public UsersStores(){

        }

        public UsersStores(int usersStoresId, int userId, int storeId){
            this.UsersStoresId = usersStoresId;
            this.UserId = userId;
            this.StoreId = storeId;
        }
    }
}
