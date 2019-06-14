using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class Store : BaseEntity {
        public int StoreId { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public int Owner { get; set; }
        public string Logo { get; set; }
        public string WarehouseLocation { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public string StoreStatusCode { get; set; }
        public double Commission { get; set; }
        public DateTime CreationDate { get; set; }

        public Store() {
        }

        public Store(int storeId, string identification, string name, int owner, string logo, string warehouseLocation, string description, string currencyCode, string storeStatusCode, double commission, DateTime creationDate) {

            try {
                StoreId = storeId;
                Identification = identification;
                Name = name;
                Owner = owner;
                WarehouseLocation = warehouseLocation;
                Logo = logo;
                Description = description;
                CurrencyCode = currencyCode;
                StoreStatusCode = storeStatusCode;
                Commission = commission;
                CreationDate = creationDate;
            } catch {
                throw new Exception("All values are required: [store_id, identification, name, owner_id, warehouse_location, logo, description, currency_code, store_status_code, commission, creation_date].");
            }
        }

        //public Store(string[] infoArray) {

        //    if (infoArray != null && infoArray.Length >= 11) {
        //        StoreId = int.Parse(infoArray[0]);
        //        Identification = infoArray[1];
        //        Name = infoArray[2];
        //        OwnerId = int.Parse(infoArray[3]);
        //        WarehouseLocation = infoArray[4];
        //        Logo = infoArray[5];
        //        Description = infoArray[6];
        //        CurrencyCode = infoArray[7];
        //        StoreStatusCode = infoArray[8];
        //        Commission = double.Parse(infoArray[9]);
        //        CreationDate = DateTime.Parse(infoArray[10]);
        //    } else {
        //        throw new Exception("All values are required: [store_id, identification, name, owner_id, warehouse_location, logo, description, currency_code, store_status_code, commission, creation_date].");
        //    }
        //}

    }
}
