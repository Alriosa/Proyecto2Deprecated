using EntitiesPOJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI {
    public class StoreManager : MasterManager {
        MasterManager mng = new MasterManager();

        public StoreManager()  {
        }

        public Store Create(Store store)  {
            store.StoreId = mng.GetNextId(store, EntityTypes.Store);
            store.StoreStatusCode = "STS01";
            if (store.Commission <= 0)
                store.Commission = 5.00;
            store.CreationDate = DateTime.Now;
            return store;
        }

    }
}
