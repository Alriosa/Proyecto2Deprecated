using System;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using WebAPI.Models;
using Exceptions;

namespace WebAPI.Controllers {
    public class CategoryController : ApiController {
        private ApiResponse apiResp;

        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Category>(EntityTypes.Category);
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult Retrieve(Category cat) {
            try {
                var mng = new MasterManager();
                cat = mng.Retrieve<Category>(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Data = cat};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(Category cat) {
            try {
                var mng = new MasterManager();
                mng.Create<Category>(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        public IHttpActionResult Update(Category cat) {
            try {
                var mng = new MasterManager();
                mng.Update(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(Category cat) {
            try {
                var mng = new MasterManager();
                mng.Delete(cat, EntityTypes.Category);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}