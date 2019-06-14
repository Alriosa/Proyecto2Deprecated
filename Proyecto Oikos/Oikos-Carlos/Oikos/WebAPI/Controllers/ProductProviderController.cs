using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [ExceptionFilter]
    public class ProductProviderController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        /*
        *This method is in charge of retrieving all the Product Providers registered in the database.
        *
        * @author Carlos Rios
        * @return The HttpMessage result of the action performed by method.
        */
        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<ProductProvider>(EntityTypes.ProductProvider);
            return Ok(apiResp);
        }

        [HttpGet]
        /*
       *This method is in charge of retrieving a Product Provider registered in the database.
       *Erick Garro
       * @param int productProviderID - An instance of the Product Providers class with the id to search for.
       * @return The HttpMessage result of the action performed by method.
       */
        public IHttpActionResult Retrieve(int productProviderId) {
            ProductProvider productProvider = new ProductProvider();
            try {
                var mng = new MasterManager();
                var lst = mng.RetrieveAll<ProductProvider>(EntityTypes.ProductProvider);

                foreach (var pp in lst) {
                    if (productProviderId == pp.ProductProviderId) {
                        productProvider = pp;
                    }
                }

                apiResp = new ApiResponse() {Data = productProvider};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult RetrieveAllByStoreId(int storeId) {
            try {
                var mng = new MasterManager();
                var lst = mng.RetrieveAll<ProductProvider>(EntityTypes.ProductProvider);
                var filteredLst = new List<ProductProvider>();
                foreach (var pp in lst) {
                    if (pp.StoreId != storeId) continue;
                    textMod.AdaptObject(pp, EntityTypes.ProductProvider, false);
                    filteredLst.Add(pp);
                }

                apiResp = new ApiResponse() {Message="", Data = filteredLst};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        [HttpPost]
        /*
       *This method is in charge of retrieving a Product Provider registered in the database.
       *
       * @author Carlos Rios
       * @param ProductProvider productProvider - An instance of the Product Providers class with the id to search for.
       * @return The HttpMessage result of the action performed by method.
       */
        public IHttpActionResult Retrieve(ProductProvider productProvider) {
            try {
                var mng = new MasterManager();
                productProvider = mng.Retrieve<ProductProvider>(productProvider, EntityTypes.ProductProvider);
                apiResp = new ApiResponse() {Data = productProvider};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        /*
        *This method is in charge of registering a new product provider in the database.
        *
        * @author Carlos Rios
        * @param ProductProvider productProvider - An instance of the Product Providers  method class with the data of the attributes of Product Providers  to be registered in the data base
        * @return The HttpMessage result of the action performed by method.
        */
        public IHttpActionResult Create(ProductProvider prod) {
            try {
                var mng = new MasterManager();
                prod.ProductProviderId = mng.GetNextId(prod, EntityTypes.ProductProvider);
                textMod.AdaptObject(prod, EntityTypes.ProductProvider, true);
                mng.Create<ProductProviderController>(prod, EntityTypes.ProductProvider);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        /*
        *This method is in charge of updating the information of a registered product provider in the database.
        *
        * @author Carlos Rios
        * @param ProductProvider productProvider - An instance of the Product Providers class with the information to be updated.
        * @return The HttpMessage result of the action performed by method.
        */
        public IHttpActionResult Update(ProductProvider prod) {
            try {
                var mng = new MasterManager();
                mng.Update(prod, EntityTypes.ProductProvider);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        /*
       *This method is in charge of deleting a Product Provider registered in the database.
       *
       * @author Carlos Rios
       * @param ProductProvider productProvider - An instance of the Product Providers class with the id to be deleted.
       * @return The HttpMessage result of the action performed by method.
       */
        public IHttpActionResult Delete(ProductProvider prod) {
            try {
                var mng = new MasterManager();
                mng.Delete(prod, EntityTypes.ProductProvider);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}