using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class ProductMedia : BaseEntity {
        public int ProductMediaId { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }

        /*
         * Default constructor of the Product Media class
         *
         * @author Erick Garro
         *
         */
        public ProductMedia() { }

        /*
         * Constructor of the Product Media class
         *
         * @author Erick Garro
         *
         * @param string productMediaId - Identification the product media
         * @param int productId - Id of the product to which the media is associated
         * @param string url - URL of the product picture
         * @param string description - Description of the product media
         * @param bool isActive - Sets the status for soft delete
         */
        public ProductMedia(int productMediaId, int productId, string url, bool isActive) {  
            ProductMediaId = productMediaId;
            ProductId = productId;
            Url = url;
            IsActive = isActive;
        }

        /*
         * Constructor of the Product Media class
         *
         * @author Erick Garro
         *
         * @param int productId - Id of the product to which the media is associated
         * @param string url - URL of the product picture
         * @param string description - Description of the product media
         * @param bool isActive - Sets the status for soft delete
         */
        public ProductMedia(int productId, string url, bool isActive) {
            ProductId = productId;
            Url = url;
            IsActive = isActive;
        }
    }
}
