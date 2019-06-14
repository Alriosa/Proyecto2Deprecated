using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesPOJO {
    public class ShoppingCart : BaseEntity {
        public int ShoppingCartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public bool IsOffer { get; set; }

        /*
         * Default constructor for the shoppingCart Store class
         *
         * @author Erick Garro
         */
        public ShoppingCart() {
        }

        /*
        * Constructor of the ShoppingCart class
        *
        * @author Erick Garro
        *
        * @param int userId - Id of the customer
        * @param int productId - Id of the product
        * @param int quantity - units of the item added into cart
        * @param double price - price for the offer
        * @param bool isActive - Status for soft deleting item in cart
        */
        public ShoppingCart(int userId, int productId, int quantity, bool isActive = true, double price = 0, bool isOffer = false) {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            IsActive = isActive;
            Price = price;
            IsOffer = isOffer;
        }

        /*
        * Constructor of the ShoppingCart class
        *
        * @author Erick Garro
        *
        * @param int shoppingCartId - Id of the shopping cart
        * @param int userId - Id of the customer
        * @param int productId - Id of the product
        * @param int quantity - units of the item added into cart
        * @param double price - price for the offer
        * @param bool isActive - Status for soft deleting item in cart
        */
        public ShoppingCart(int shoppingCartId, int userId, int productId, int quantity, bool isActive, double price = 0, bool isOffer = false) {
            ShoppingCartId = shoppingCartId;
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            IsActive = isActive;
            Price = price;
            IsOffer = isOffer;
        }
    }
}