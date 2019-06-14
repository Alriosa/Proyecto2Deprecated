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
    public class ProductCategoriesController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        /*
         *This method is in charge of retrieving all the responses of a request registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAllByProduct(int productId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<ProductsCategories>();
            var data = mng.RetrieveAll<ProductsCategories>(EntityTypes.ProductsCategories);
            foreach (var obj in data)
                if (obj.ProductId == productId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) textMod.AdaptObject(resp, EntityTypes.ProductsCategories, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        /*
         *This method is in charge of retrieving a response registered in the database.
         *
         * @author Leonardo Mora
         * @param ProductRequestResponses resp - An instance of the ProductRequestResponses class with the id to search for.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Retrieve(int id) {
            try {
                var mng = new MasterManager();
                var prodCat = mng.Retrieve<ProductsCategories>(new ProductsCategories {ProductId = id},
                    EntityTypes.ProductsCategories);
                textMod.AdaptObject(prodCat, EntityTypes.ProductsCategories, false);
                apiResp = new ApiResponse() {Data = prodCat};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        /*
         *This method is in charge of registering a new response in the database.
         *
         * @author Leonardo Mora
         * @param ProductRequestResponses resp - An instance of the ProductRequestResponses class with the information of the category to be registered.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Create(ProductsCategories prodCat) {
            try {
                var mng = new MasterManager();
                if (prodCat.ProductsCategoriesId == 0)
                    prodCat.ProductsCategoriesId = mng.GetMaxId(prodCat, EntityTypes.ProductsCategories) + 1;
                textMod.AdaptObject(prodCat, EntityTypes.ProductsCategories, true);
                mng.Create<ProductsCategories>(prodCat, EntityTypes.ProductsCategories);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPost]
        /*
         * This methods allows the user to create multiple categories for a product.
         *
         * @author Leonardo Mora
         * @param List<ProductsCategories> prodCatLst - List with the categories to be added.
         * @return Result of the operations.
         */
        public IHttpActionResult CreateMultiple(List<ProductsCategories> prodCatLst) {
            var mng = new MasterManager();
            try {
                foreach (var prodCat in prodCatLst) {
                    if (prodCat.ProductsCategoriesId == 0)
                        prodCat.ProductsCategoriesId = mng.GetMaxId(prodCat, EntityTypes.ProductsCategories) + 1;
                    mng.Create<ProductsCategories>(prodCat, EntityTypes.ProductsCategories);
                }

                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        /*
         *This method is in charge of updating the information of a registered category in the database.
         *
         * @author Leonardo Mora
         * @param ProductRequestResponses resp - An instance of the ProductRequestResponses class with the information to be updated.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Update(ProductsCategories prodCat) {
            try {
                var mng = new MasterManager();
                textMod.AdaptObject(prodCat, EntityTypes.ProductsCategories, true);
                mng.Update(prodCat, EntityTypes.ProductsCategories);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}