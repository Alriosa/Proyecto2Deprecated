using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class ProductRequestResponses : BaseEntity {
        public int ProductRequestResponseId { get; set; }
        public int ProductRequestId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDatetime { get; set; }
        public bool IsAccepted { get; set; }

        /*
         * Constructor of the ProductRequestResponses class
         *
         * @author Leonardo Mora
         */
        public ProductRequestResponses() {
        }

        /*
         * Constructor of the ProductRequestResponses class
         *
         * @author Leonardo Mora
         * @param int productRequestResponseId - Id of the response.
         * @param int productRequestId - Id of the request.
         * @param int productId - Id of the product.
         * @param double price - Price offered.
         * @param int quantity - Quantity offered.
         * @param string description - Description of the response.
         * @param DateTime responseDatetime - Date and time when the response was given.
         * @param bool isAccepted - If the response has been accepted or not.
         */
        public ProductRequestResponses(int productRequestResponseId, int productRequestId, int productId,
            double price, int quantity, string description, DateTime responseDatetime, bool isAccepted) {
            ProductRequestResponseId = productRequestResponseId;
            ProductRequestId = productRequestId;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Description = description;
            ResponseDatetime = responseDatetime;
            IsAccepted = isAccepted;
        }
    }
}