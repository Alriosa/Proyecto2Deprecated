using CoreAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class ProductController : ApiController {
        ApiResponse apiResp = new ApiResponse();

        /**
            * This method retrieves all products registered in the database as an api response
            * @author Tremi
            * @param No params
            * @return returns an api response with a list of all the products in the system
         */
        //GET api/product/RetrieveAllProducts
        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var lst = mng.RetrieveAll<Product>(EntityTypes.Product);
            List<Product> data = new List<Product>();
            foreach (var product in lst) {
                if (product.IsActive == true) {
                    data.Add(product);
                }
            }

            apiResp.Data = data;

            return Ok(apiResp);
        }

        [HttpGet]
        /*
         * This method is in charge of retrieving the last id added to the products categories table.
         *
         * @author Leonardo Mora
         * @return The id of the last productadded.
         */
        public IHttpActionResult RetrieveLastId() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.GetMaxId(new Product(),EntityTypes.Product);
            return Ok(apiResp);
        }

        /**
            * This method retrieves all products associated to a store
            * @author Tremi
            * @param Id of the store
            * @return returns an api response with a list of all the products registered to a store
        */
        //GET api/product/RetrieveStoreProducts?storeId={store id}
        [HttpGet]
        public IHttpActionResult RetrieveStoreProducts(int storeId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var lst = mng.RetrieveAll<Product>(EntityTypes.Product);
            List<Product> data = new List<Product>(); 
            foreach (var product in lst){
                if (storeId == product.StoreId && product.IsActive == true) {
                    data.Add(product);
                }
            }

            apiResp.Data = data;

            return Ok(apiResp);
        }

        /**
           * This method retrieves the name and store id to which a product is associated to
           * @author Erick Garro
           * @param Id of the product
           * @return returns an api response with a list of all the products registered to a store
       */
        //GET api/product/RetrieveStoreInfo?productId={productId}
        [HttpGet]
        public IHttpActionResult RetrieveStoreInfo(int productId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            Product product = new Product();
            product.ProductId = productId;

            var dbProduct = mng.Retrieve<Product>(product, EntityTypes.Product);
            var lst = mng.RetrieveAll<Store>(EntityTypes.Store);

            foreach (var s in lst) {
                if (dbProduct.StoreId == s.StoreId) {
                    apiResp.Data = s;
                }
            }
           return Ok(apiResp);
        }

        /**
            * This method retrieves a product by it's identification
            * @author Tremi
            * @param pId - string wit id of the product requested
            * @returns apiResponse with product requested 
        */
        //GET api/product/RetrieveById?pId={productId}
        [HttpGet]
        public IHttpActionResult RetrieveById(int pId) {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                Product product = new Product();
                product.ProductId = pId;
                apiResp.Data = mng.Retrieve<Product>(product, EntityTypes.Product);

                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }

        /**
           * This method creates a product 
           * @author Tremi
           * @param pProduct - Objeto tipo producto con sus atributos obligatorios 
           * @returns apiResponse confirming action
       */
        //POST api/product/CreateProduct
        [HttpPost]
        public IHttpActionResult Create(Product pProduct) {
            try {
                var mng = new MasterManager();
                pProduct.ProductId = mng.GetNextId(pProduct, EntityTypes.Product);
                mng.Create<Product>(pProduct, EntityTypes.Product);

                apiResp = new ApiResponse {Message = "Product was created."};

                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
           * This method updates the information of a product
           * @author Tremi
           * @param pProduct - Product object
           * @returns apiResponse confirming action
       */
        //PUT api/product/UpdateProduct
        [HttpPut]
        public IHttpActionResult Update(Product pProduct) {
            try {
                var mng = new MasterManager();
                mng.Update(pProduct, EntityTypes.Product);

                apiResp = new ApiResponse();
                apiResp.Message = "Product was updated.";

                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
            * This method soft deletes a product
            * @author Tremi
            * @param pProduct - Product object
            * @returns apiResponse confirming action
        */
        //DELETE api/product/DeleteProduct
        [HttpDelete]
        public IHttpActionResult Delete(Product pProduct) {
            try {
                var mng = new MasterManager();

                var productToDelete = mng.Retrieve<Product>(pProduct, EntityTypes.Product);
                productToDelete.IsActive = false;

                mng.Update(productToDelete, EntityTypes.Product);

                apiResp = new ApiResponse {Message = "Product deleted."};

                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
    * This method retrieves the last id for a registered product
    * @author Erick Garro
    * @returns apiResponse with product requested 
*/
        //GET api/product/RetrieveLastProductId
        [HttpGet]
        public IHttpActionResult RetrieveLastProductId() {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                Product product = new Product();
                apiResp.Data = mng.GetMaxId(product, EntityTypes.Product);

                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }

        [HttpGet]
        /*
         *This method retrieves all products that match the id's in a given string.
         *
         * @author Leonardo Mora
         * @param string ids - Ids of the products requested.
         * @return An API Response with the requested products
         */
        public IHttpActionResult RetrieveMultiple(string ids) {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                var prodLst = new List<Product>();
                foreach (var i in ids.Split(',')) {
                    prodLst.Add(mng.Retrieve<Product>(new Product {ProductId = int.Parse(i)}, EntityTypes.Product));
                }

                apiResp.Data = prodLst;

                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }
    }
}