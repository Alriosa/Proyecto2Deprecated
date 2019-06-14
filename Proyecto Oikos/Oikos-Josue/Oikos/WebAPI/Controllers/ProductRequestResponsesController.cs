using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class ProductRequestResponsesController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        /*
         *This method is in charge of retrieving all the responses of a request registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<ProductRequestResponses>(EntityTypes.ProductRequestResponses);
            foreach (var resp in data) textMod.AdaptObject(resp, EntityTypes.ProductRequestResponses, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        /*
         *This method is in charge of retrieving all the responses of a request registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAllByRequest(int productRequestId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<ProductRequestResponses>();
            var data = mng.RetrieveAll<ProductRequestResponses>(EntityTypes.ProductRequestResponses);
            foreach (var obj in data)
                if (obj.ProductRequestId == productRequestId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) textMod.AdaptObject(resp, EntityTypes.ProductRequestResponses, false);
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
                var resp = mng.Retrieve<ProductRequestResponses>(new ProductRequestResponses{ProductRequestResponseId = id}, EntityTypes.ProductRequestResponses);
                textMod.AdaptObject(resp, EntityTypes.ProductRequestResponses, false);
                apiResp = new ApiResponse() {Data = resp};
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
        public IHttpActionResult Create(ProductRequestResponses resp) {
            try {
                var mng = new MasterManager();
                if (resp.ProductRequestResponseId == 0)
                    resp.ProductRequestResponseId = mng.GetMaxId(resp, EntityTypes.ProductRequestResponses) + 1;
                textMod.AdaptObject(resp, EntityTypes.ProductRequestResponses, true);
                mng.Create<ProductRequestResponses>(resp, EntityTypes.ProductRequestResponses);
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
        public IHttpActionResult Update(ProductRequestResponses resp) {
            try {
                var mng = new MasterManager();
                textMod.AdaptObject(resp, EntityTypes.ProductRequestResponses, true);
                mng.Update(resp, EntityTypes.ProductRequestResponses);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}