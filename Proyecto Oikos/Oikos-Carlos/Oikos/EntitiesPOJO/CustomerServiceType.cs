using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class CustomerServiceType : BaseEntity {
        public int CustomerServiceTypeId { get; set; }
        public string Description { get; set; }
        public bool AcceptsRefunds { get; set; }
        public bool IsActive { get; set; }

        /*
         * Constructor of the CustomerServiceType class
         *
         * @author Erick Garro
         */
        public CustomerServiceType() {
        }

        /*
         * Constructor of the CustomerServiceType class
         *
         * @author Erick Garro
         *
         * @param int customerServiceTypeId - Id of the customer service type
         * @param string description - Description of the customer service type
         * @param bool acceptsRefunds - Determines if the customer service type accepts refunds automatically
         * @param bool isActive - Sets the CustomerServiceType to active or inactive for soft deletes
         */
        public CustomerServiceType(int customerServiceTypeId, string description, bool acceptsRefunds, bool isActive) {

            try {
                CustomerServiceTypeId = customerServiceTypeId;
                Description = description;
                AcceptsRefunds = acceptsRefunds;
                IsActive = isActive;
            }
            catch {
                throw new Exception("All values are required: [customer_service_type_id, description, accepts_refunds, is_active].");
            }
        }

        /*
         * Constructor of the CustomerServiceType class
         *
         * @author Erick Garro
         *
         * @param string description - Description of the customer service type
         * @param bool acceptsRefunds - Determines if the customer service type accepts refunds automatically
         */
        public CustomerServiceType(string description, bool acceptsRefunds) {

            try {
                Description = description;
                AcceptsRefunds = acceptsRefunds;
            }
            catch {
                throw new Exception("All values are required: [description, accepts_refunds");
            }
        }

        /*
         * Constructor of the CustomerServiceType class by customerServiceTypeId only
         *
         * @author Erick Garro
         *
         * @param int customerServiceTypeId - Id of the customer service type
         */
        public CustomerServiceType(int customerServiceTypeId) {

            try {
                CustomerServiceTypeId = customerServiceTypeId;
            }
            catch {
                throw new Exception("All values are required: [customer_service_type_id]");
            }
        }

    }

}
