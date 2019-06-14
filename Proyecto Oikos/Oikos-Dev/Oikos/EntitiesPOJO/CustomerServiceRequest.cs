 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class CustomerServiceRequest : BaseEntity {
        public int CustomerServiceRequestId { get; set; }
        public int OrderId { get; set; }
        public int CustomerServiceTypeId { get; set; }
        public string Description { get; set; }
        public DateTime RequestDatetime { get; set; }
        public bool IsResolved { get; set; }
        public bool IsActive { get; set; }

        /*
         * Constructor of the CustomerServiceRequest class
         *
         * @author Erick Garro
         */
        public CustomerServiceRequest() {
        }

        /*
         * Constructor of the CustomerServiceRequest class
         *
         * @author Erick Garro
         *
         * @param int orderId - ID of the associated Order
         * @param int customerServiceTypeId - Customer service request type ID
         * @param string description - Description of the customer service request
         * @param DateTime requestDatetime - Date and time of the customer service request
         */
        public CustomerServiceRequest(int orderId, int customerServiceTypeId, string description, DateTime requestDatetime) {

            try {
                OrderId = orderId;
                CustomerServiceTypeId = customerServiceTypeId;
                Description = description;
                RequestDatetime = requestDatetime;
            }
            catch {
                throw new Exception("All values are required: [order_id, customer_service_type_id, description, request_datetime].");
            }
        }

        /*
         * Constructor of the CustomerServiceRequest class
         *
         * @author Erick Garro
         *
         * @param int customerServiceRequestId - Customer service request ID
         * @param int orderId - ID of the associated Order
         * @param int customerServiceTypeId - Customer service request type ID
         * @param string description - Description of the customer service request
         * @param DateTime requestDatetime - Date and time of the customer service request
         */
        public CustomerServiceRequest(int customerServiceRequestId, int orderId, int customerServiceTypeId, string description, DateTime requestDatetime) {

            try {
                CustomerServiceRequestId = customerServiceRequestId;
                OrderId = orderId;
                CustomerServiceTypeId = customerServiceTypeId;
                Description = description;
                RequestDatetime = requestDatetime;
            }
            catch {
                throw new Exception("All values are required: [customer_service_request_id, order_id, customer_service_type_id, description, request_datetime].");
            }
        }

        /*
         * Constructor of the CustomerServiceRequest class
         *
         * @author Erick Garro
         *
         * @param int customerServiceRequestId - Customer service request ID
         * @param int orderId - ID of the associated Order
         * @param int customerServiceTypeId - Customer service request type ID
         * @param string description - Description of the customer service request
         * @param DateTime requestDatetime - Date and time of the customer service request
         * @param bool isResolved - Sets the CustomerServiceRequest resolution to true or false
         * @param bool isActive - Soft deletes CustomerServiceRequest resolution to true or false
         */
        public CustomerServiceRequest(int customerServiceRequestId, int orderId, int customerServiceTypeId, string description, DateTime requestDatetime, bool isResolved, bool isActive) {

            try {
                CustomerServiceRequestId = customerServiceRequestId;
                OrderId = orderId;
                CustomerServiceTypeId = customerServiceTypeId;
                Description = description;
                RequestDatetime = requestDatetime;
                IsResolved = isResolved;
                IsActive = isActive;
            }
            catch {
                throw new Exception("All values are required: [customer_service_request_id, order_id, customer_service_type_id, description, request_datetime, is_resolved, is_active].");
            }
        }

        /*
         * Constructor of the CustomerServiceRequest class
         *
         * @author Erick Garro
         *
         * @param int customerServiceRequestId - Customer service request ID
         */
        public CustomerServiceRequest(int customerServiceRequestId) {

            try {
                CustomerServiceRequestId = customerServiceRequestId;
            }
            catch {
                throw new Exception("All values are required: [customer_service_request_id].");
            }
        }

    }

}
