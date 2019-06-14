using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EntitiesPOJO {

    public class ShoppingBigCart : BaseEntity{
        public int ShoppingCartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public string ProductMediaUrl { get; set; }
        public double UnitPrice { get; set; }
        public double Tax { get; set; }
        public int UnitsAvailable { get; set; }
        public int Units { get; set; }
        public double ItemSubtotal { get; set; }
        public bool IsOffer { get; set; }
        public bool IsActive { get; set; }

        /*
         * Default class constructor
         *
         * @author Erick Garro
         */
        public ShoppingBigCart() {
        }

        /*
         * Class constructor
         *
         * @author Erick Garro
         *
         * @param int shoppingCartId - user id to which a location is associated
         * @param int userId - user id to whom the cart item belongs to
         * @param int productId - user id to which a location is associated
         * @param string productName - unit price
         * @param int productId - user
         * @param string productMediaUrl - product image
         * @param double tax - tax percentage 100.00%
         * @param int unitsAvailable - units available in stock for this product 
         * @param int units - quantity of units per line
         * @param double itemsSubtotal - line subtotal
         * @param bool isOffer - tells if the product is an offer
         * @param bool isActive - item status for soft delete
         */
        public ShoppingBigCart(int shoppingCartId, int userId, int productId, string productName, string productMediaUrl, double unitPrice, double tax, int unitsAvailable, int units, double itemSubtotal, bool isOffer, bool isActive) {
            ShoppingCartId = shoppingCartId;
            UserId = userId;
            ProductId = productId;
            ProductName = productName;
            ProductUrl = "/Product?=productId=" + productId;
            ProductMediaUrl = productMediaUrl;
            UnitPrice = unitPrice;
            Tax = tax;
            UnitsAvailable = unitsAvailable;
            Units = units;
            ItemSubtotal = itemSubtotal;
            IsOffer = isOffer;
            IsActive = isActive;
        }

    }


}



