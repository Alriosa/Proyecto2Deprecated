using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Antlr.Runtime.Misc;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [ExceptionFilter]
    public class ShoppingCartController : ApiController {
        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();
        private readonly TextModule textMod = new TextModule();

        [HttpPost]
        // GET POST /ShoppingCart/Create
        /*
         *This method registers a item in a shopping cart in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(ShoppingCart item) {
            try {
                List<ShoppingCart> lst = new List<ShoppingCart>();
                lst = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);
                var found = false;

                foreach (var i in lst) {
                    if (item.UserId == i.UserId && item.ProductId == i.ProductId && i.IsActive == true) {
                        found = true;

                        if (validateStockQuantity(i, lst) == true) {
                            i.Quantity += item.Quantity;
                            mng.Update(i, EntityTypes.ShoppingCart);
                            apiResp = new ApiResponse()
                                {Message = "El producto ya estaba en el carrito y se incrementó la cantidad."};
                        }
                        else {
                            apiResp = new ApiResponse() {Message = "No hay unidades suficientes en inventario."};
                        }
                    }
                }

                if (found == false) {
                    item.ShoppingCartId = mng.GetNextId(item, EntityTypes.ShoppingCart);
                    item.IsActive = true;
                    if (validateStockQuantity(item, lst) == true) {
                        mng.Create<ShoppingCart>(item, EntityTypes.ShoppingCart);
                        apiResp = new ApiResponse() {Message = "El producto fue añadido al carrito."};
                    } 
                }
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/ShoppingCart/RetrieveAll
        /*
         *This method retrieves all the shopping cart items registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            List<ShoppingCart> lst = new List<ShoppingCart>();

            try {
                var lstShoppingCarts = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);
                foreach (var s in lstShoppingCarts) {
                    if (s.IsActive == true) {
                        lst.Add(s);
                    }
                }

                apiResp.Data = lst;
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
        * This method retrieves all items associated to a customer
        * @author Erick Garro
        * @param int userId - Id of an user
        * @return the IHttpActionResult result of the action performed by method.
        */
        //GET api/ShoppingCart/RetrieveShoppingCart?userId={int userId}
        [HttpGet]
        public IHttpActionResult RetrieveShoppingCart(int userId) {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                var lst = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);
                List<ShoppingCart> data = new List<ShoppingCart>();

                foreach (var item in lst) {
                    if (userId == item.UserId && item.IsActive == true) {
                        data.Add(item);
                    }
                }

                apiResp.Message = "OK";
                apiResp.Data = data;
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /*
        * This method retrieves the count of items in a customer's shopping cart
        * @author Erick Garro
        * @param int userId - Id of an user
        * @return the IHttpActionResult result of the action performed by method.
        */
        //GET api/ShoppingCart/RetrieveItemsCount?userId={int userId}
        [HttpGet]
        public IHttpActionResult RetrieveItemsCount(int userId) {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                var lst = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);
                int count = 0;

                foreach (var item in lst) {
                    if (userId == item.UserId && item.IsActive == true) {
                        count++;
                    }
                }

                apiResp.Message = "OK";
                apiResp.Data = count;
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }

        /**
       * This method updates the information of an item in a shopping cart
       * @author Erick Garro
       * @param ShoppingCart item - a ShoppingCart object
       * @returns apiResponse confirming action
       */
        //PUT api/ShoppingCart/Update
        [HttpPut]
        public IHttpActionResult Update(ShoppingCart item) {
            try {
                var mng = new MasterManager();
                mng.Update(item, EntityTypes.ShoppingCart);

                apiResp = new ApiResponse();
                apiResp.Message = "Shopping cart item was updated.";

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
        * This method soft deletes an item in the shopping cart
        * @author Erick Garro
        * @param item - ShoppingCart object
        * @returns apiResponse confirming action
        */
        //DELETE api/ShoppingCart/Delete
        [HttpDelete]
        public IHttpActionResult Delete(ShoppingCart item) {
            try {
                var mng = new MasterManager();

                var itemToDelete = mng.Retrieve<ShoppingCart>(item, EntityTypes.ShoppingCart);
                itemToDelete.IsActive = false;

                mng.Update(itemToDelete, EntityTypes.ShoppingCart);

                apiResp = new ApiResponse {
                    Message = "Item deleted."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
        * This method soft deletes an item in the shopping cart
        * @author Erick Garro
        * @param item - ShoppingCart object
        * @returns apiResponse confirming action
        */
        //DELETE api/ShoppingCart/DeleteAllByCustomerId?userId={int userId}
        [HttpDelete]
        public IHttpActionResult DeleteAllByCustomerId(int userId) {
            try {
                var mng = new MasterManager();
                var lst = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);

                foreach (var item in lst) {
                    if (userId == item.UserId && item.IsActive == true) {
                        item.IsActive = false;
                        mng.Update(item, EntityTypes.ShoppingCart);
                    }
                }

                apiResp = new ApiResponse {
                    Message = "Items deleted."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
        * This method validates if is a product is in customers shoppingcartalready
        * @author Erick Garro
        * @param int productId - item to evaluate
        * @param int userId - the active user's ID
        * @returns bool
        */
        // GET POST /ShoppingCart/IsProductInCart?productId={productId}&userId={userId}
        [HttpGet]
        public IHttpActionResult IsProductInCart(int productId, int userId) {
            var lstCart = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);
            var IsInInventory = false;

            try {
                apiResp = new ApiResponse();

                foreach (var item in lstCart) {
                    if (productId == item.ProductId && userId == item.UserId && item.IsActive == true) {
                        IsInInventory = true;
                    }
                }

                if (IsInInventory == true) {
                    apiResp.Data = IsInInventory;
                    apiResp.Message = "Found";
                } else {
                    apiResp.Message = "Not found";
                }
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
            return Ok(apiResp);
        }

        /*
        * This method retrieves all items associated to a customer
        * @author Erick Garro
        * @param int userId - Id of an user
        * @return the IHttpActionResult result of the action performed by method.
        */
        //GET api/ShoppingCart/RetrieveShoppingCart?userId={int userId}
        [HttpGet]
        public IHttpActionResult RetrieveShoppingBigCart(int userId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            ShoppingBigCart item;


            try {
                List<ShoppingCart> lstShoppingCart = new List<ShoppingCart>();
                List<Product> lstProduct = new List<Product>();
                List<ProductMedia> lstProductMedia = new List<ProductMedia>();
                List<Inventory> lstInventory = new List<Inventory>();
                List<ShoppingBigCart> data = new List<ShoppingBigCart>();

                var lstInventoryDb = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
                var lstShoppingCartDb = mng.RetrieveAll<ShoppingCart>(EntityTypes.ShoppingCart);
                var lstProductDb = mng.RetrieveAll<Product>(EntityTypes.Product);
                var lstProductMediaDb = mng.RetrieveAll<ProductMedia>(EntityTypes.ProductMedia);

                foreach (var i in lstShoppingCartDb) {
                    if (userId == i.UserId && i.IsActive) {
                        lstShoppingCart.Add(i);
                    }
                }

                foreach (var i in lstProductDb) {
                    if (i.IsActive) {
                        lstProduct.Add(i);
                    }
                }

                foreach (var i in lstInventoryDb) {
                    if (i.IsSold == false) {
                        lstInventory.Add(i);
                    }
                }

                foreach (var i in lstProductMediaDb) {
                    if (i.IsActive) {
                        textMod.AdaptObject(i, EntityTypes.ProductMedia, false);
                        lstProductMedia.Add(i);
                    }
                }

                foreach (var sc in lstShoppingCart) {
                    item = new ShoppingBigCart();
                    item.ShoppingCartId = sc.ShoppingCartId;
                    item.UserId = sc.UserId;
                    item.ProductId = sc.ProductId;
                    item.Units = sc.Quantity;
                    item.IsOffer = sc.IsOffer;
                    item.IsActive = sc.IsActive;
                    item.IsOffer = sc.IsOffer;
                    item.ProductUrl = "/Product?productId=" + sc.ProductId;

                    foreach (var p in lstProduct) {
                        if (item.ProductId == p.ProductId) {
                            item.ProductName = p.Name;
                            item.Tax = (double) p.TaxPercentage;
                            if (sc.IsOffer) {
                                item.UnitPrice = sc.Price / sc.Quantity;
                            } else {
                                item.UnitPrice = (double)p.SellingPrice;
                            }
                        }
                    }

                    foreach (var pm in lstProductMedia) {
                        if (sc.ProductId == pm.ProductId) {
                            item.ProductMediaUrl = pm.Url;
                            break;
                        }
                    }

                    if (sc.IsOffer) {
                        item.UnitsAvailable = sc.Quantity;
                        item.ItemSubtotal = sc.Price;
                    }
                    else {
                        foreach (var unit in lstInventory) {
                            if (sc.ProductId == unit.ProductId) {
                                item.UnitsAvailable++;
                            }
                        }
                        item.ItemSubtotal = item.Units * item.UnitPrice;
                    }
                    
                    item.ItemSubtotal = item.Units * item.UnitPrice;
                    data.Add(item);
                }

                
                apiResp.Message = "OK";
                apiResp.Data = data;
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
            return Ok(apiResp);
        }

        /**
        * This method validates if the desired quantity of a product has enough
        * units in inventory to add it to the shopping cart
        * @author Erick Garro
        * @param ShoppingCart item - item to be added (includes quantity)
        * @param List<ShoppingCart> lstCart - list of active products in cart
        * @returns bool
        */
        private bool validateStockQuantity(ShoppingCart item, List<ShoppingCart> lstCart) {
            var lstInventory = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
            var stockQuantity = 0;
            var cartQuantity = 0;
            var valid = false;
            var itemsQuantityAvailableInStock = 0;

            foreach (var unit in lstInventory) {
                if (unit.ProductId == item.ProductId && unit.IsSold == false) {
                    stockQuantity++;
                }
            }

            foreach (var i in lstCart) {
                if (item.ProductId == i.ProductId && item.UserId == i.UserId && i.IsActive == true) {
                    cartQuantity = i.Quantity;
                }
            }

            itemsQuantityAvailableInStock = stockQuantity - cartQuantity;

            if (itemsQuantityAvailableInStock > 0 && item.Quantity <= itemsQuantityAvailableInStock) {
                valid = true;
            }
            return valid;
        }
    }
}
