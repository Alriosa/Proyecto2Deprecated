using CoreAPI;
using Entities_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EntitiesPOJO;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [ExceptionFilter]
    public class StoreController : ApiController {

        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();

        [HttpPost]
        // GET POST /store/Create
        public IHttpActionResult Create(Store store) {

            try {
                if (store.Commission < 0)
                    store.Commission = 0.05;
                mng.Create<Store>(store, EntityTypes.Store);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/store/RetrieveAll
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.RetrieveAll<Store>(EntityTypes.Store);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/store/Retrieve
        public IHttpActionResult Retrieve(Store store) {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.Retrieve<Store>(store, EntityTypes.Store);
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        // PUT /store/Update
        public IHttpActionResult Update(Store store)  {
            apiResp = new ApiResponse();

            try {
                mng.Update(store, EntityTypes.Store);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpDelete]
        // DELETE api/store/Delete
        public IHttpActionResult Delete(Store store) {
            apiResp = new ApiResponse();

            try {
                mng.Delete(store, EntityTypes.Store);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}