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
        public double WarehouseLatitude { get; set; }
        public double WarehouseLongitude { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public string StoreStatusCode { get; set; }
        public double Commission { get; set; }
        public DateTime CreationDate { get; set; }

        /*
         * Constructor of the Store class
         *
         * @author Erick Garro
         */
        public Store() {
        }

        /*
         * Constructor of the Store class
         *
         * @author Erick Garro
         *
         * @param string identification - Identification number of the store (cédula jurídica)
         * @param string name - Name of the store
         * @param int owner - Id of the store owner
         * @param string logo - URL of the store's logotype
         * @param string description - Description of the store
         * @param string currencyCode - Currency used on the store by default
         * @param string storeStatusCode - Status of the store (STS00: inactive, STS01: active, STS02 suspended)
         * @param double commission - Percentage charged for each sale by the platform
         */
        public Store(string identification, string name, int owner, string logo, string description, string currencyCode, string storeStatusCode) {

            try {
                Identification = identification;
                Name = name;
                Owner = owner;
                Logo = logo;
                Description = description;
                CurrencyCode = currencyCode;
                StoreStatusCode = storeStatusCode;
            }
            catch {
                throw new Exception("All values are required: [identification, name, owner_id, logo, description, currency_code, store_status_code].");
            }
        }

        /*
         * Constructor of the Store class
         *
         * @author Erick Garro
         *
         * @param string identification - Identification number of the store (cédula jurídica)
         * @param string name - Name of the store
         * @param int owner - Id of the store owner
         * @param string logo - URL of the store's logotype
         * @param string description - Description of the store
         * @param string currencyCode - Currency used on the store by default
         * @param string storeStatusCode - Status of the store (STS00: inactive, STS01: active, STS02 suspended)
         * @param double commission - Percentage charged for each sale by the platform
         */
        public Store(string identification, string name, int owner, string logo, string description, string currencyCode, string storeStatusCode, double commission) {

            try {
                Identification = identification;
                Name = name;
                Owner = owner;
                Logo = logo;
                Description = description;
                CurrencyCode = currencyCode;
                StoreStatusCode = storeStatusCode;
                Commission = commission;
            }
            catch {
                throw new Exception("All values are required: [identification, name, owner_id, logo, description, currency_code, store_status_code, commission].");
            }
        }

        /*
         * Constructor of the Store class
         *
         * @author Erick Garro
         *
         * @param int storeId - Id of the store
         * @param string identification - Identification number of the store (cédula jurídica)
         * @param string name - Name of the store
         * @param int owner - Id of the store owner
         * @param string logo - URL of the store's logotype
         * @param double warehouseLatitude - Latitude coordinates fo the store's warehouse
         * @param double warehouseLongitude - Longitude coordinates fo the store's warehouse
         * @param string description - Description of the store
         * @param string currencyCode - Currency used on the store by default
         * @param string storeStatusCode - Status of the store (STS00: inactive, STS01: active, STS02 suspended)
         * @param double commission - Percentage charged for each sale by the platform
         * @param DateTime creationDate - Creation date of the store
         */
        public Store(int storeId, string identification, string name, int owner, string logo, double warehouseLatitude, double warehouseLongitude, string description, string currencyCode, string storeStatusCode, double commission, DateTime creationDate) {

            try {
                StoreId = storeId;
                Identification = identification;
                Name = name;
                Owner = owner;
                WarehouseLatitude = warehouseLatitude;
                WarehouseLongitude = warehouseLongitude;
                Logo = logo;
                Description = description;
                CurrencyCode = currencyCode;
                StoreStatusCode = storeStatusCode;
                Commission = commission;
                CreationDate = creationDate;
            }
            catch {
                throw new Exception("All values are required: [store_id, identification, name, owner_id, warehouse_latitude, warehouse_longitude, logo, description, currency_code, store_status_code, commission, creation_date].");
            }
        }

    }
}
