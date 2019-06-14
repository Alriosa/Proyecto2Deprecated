using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [ExceptionFilter]
    public class ProductMediaController : ApiController {
        private readonly TextModule textMod = new TextModule();
        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();

        [HttpPost]
        // GET POST /productMedia/Create
        /*
         *This method creates a new media type in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(ProductMedia productMedia) {

            try {
                productMedia.ProductMediaId = mng.GetNextId(productMedia, EntityTypes.ProductMedia);
                textMod.AdaptObject(productMedia, EntityTypes.ProductMedia, true);
                mng.Create<ProductMedia>(productMedia, EntityTypes.ProductMedia);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/productMedia/RetrieveAll
        /*
         *This method retrieves all the media types registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();

            try {
                var lstDB = mng.RetrieveAll<ProductMedia>(EntityTypes.ProductMedia);
                var lstFiltered = new List<ProductMedia>();
                foreach (ProductMedia pm in lstDB) {
                    if (true == pm.IsActive) {
                        textMod.AdaptObject(pm, EntityTypes.ProductMedia, false);
                        lstFiltered.Add(pm);
                    }
                }
                apiResp.Data = lstFiltered;
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/productMedia/RetrieveAllByProductId?productID=
        /*
         *This method retrieves all the media types registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAllByProductId(int productID) {
            apiResp = new ApiResponse();

            try {
                var lstDB = mng.RetrieveAll<ProductMedia>(EntityTypes.ProductMedia);
                var lstFiltered = new List<ProductMedia>();
                foreach (ProductMedia pm in lstDB) {
                    if (true == pm.IsActive && productID == pm.ProductId) {
                        lstFiltered.Add(pm);
                        textMod.AdaptObject(pm, EntityTypes.ProductMedia, false);
                    }
                }
                apiResp.Data = lstFiltered;
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/productMedia/Retrieve
        /*
         *This method retrieves a media type registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Retrieve(ProductMedia productMedia) {
            apiResp = new ApiResponse();

            try {
                var pm = mng.Retrieve<ProductMedia>(productMedia, EntityTypes.ProductMedia);
                textMod.AdaptObject(pm, EntityTypes.ProductMedia, false);
                apiResp.Data = pm;
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        [HttpPut]
        // PUT api/productMedia/Update
        /*
         *This method updates an existing media type in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Update(ProductMedia productMedia) {
            try {
                var mng = new MasterManager();
                textMod.AdaptObject(productMedia, EntityTypes.ProductMedia, true);
                mng.Update(productMedia, EntityTypes.ProductMedia);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }


        [HttpDelete]
        // DELETE api/productMedia/Delete
        /*
         *This method performs a soft delete of an existing media type in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Delete(ProductMedia productMedia) {
            ProductMedia newCst = new ProductMedia();

            try {
                var mng = new MasterManager();
                var lstDB = mng.RetrieveAll<ProductMedia>(EntityTypes.ProductMedia);
                textMod.AdaptObject(productMedia, EntityTypes.ProductMedia, true);

                foreach (ProductMedia pm in lstDB) {
                    
                    if (productMedia.ProductId == pm.ProductId && productMedia.Url == pm.Url) {
                        pm.IsActive = false;
                        mng.Update(pm, EntityTypes.ProductMedia);
                    }   
                }

                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }

            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}

