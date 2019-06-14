using System;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class RoleController : ApiController{
        private ApiResponse apiResp;

        [HttpGet]
        /*
         *This method is in charge of retrieving all the roles registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Role>(EntityTypes.Role);
            return Ok(apiResp);
        }

        [HttpPost]
        /*
         *This method is in charge of retrieving a role  registered in the database.
         *
         * @author Leonardo Mora
         * @param Role role - An instance of the Role class with the id to search for.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Retrieve(Role role) {
            try {
                var mng = new MasterManager();
                role = mng.Retrieve<Role>(role, EntityTypes.Role);
                apiResp = new ApiResponse() { Data = role };
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        /*
         *This method is in charge of registering a new role in the database.
         *
         * @author Leonardo Mora
         * @param Role role - An instance of the Role class with the information of the role to be registered.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Create(Role role) {
            try {
                var mng = new MasterManager();
                if (role.RoleId == 0) role.RoleId = mng.GetMaxId(role, EntityTypes.Role) + 1;
                mng.Create<Role>(role, EntityTypes.Role);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        /*
         *This method is in charge of updating the information of a registered role in the database.
         *
         * @author Leonardo Mora
         * @param Role role - An instance of the Role class with the information to be updated.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Update(Role role) {
            try {
                var mng = new MasterManager();
                mng.Update(role, EntityTypes.Role);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        /*
         *This method is in charge of deleting a role registered in the database.
         *
         * @author Leonardo Mora
         * @param Role role - An instance of the Role class with the id to be deleted.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Delete(Role role) {
            try {
                var mng = new MasterManager();
                role.IsActive = false;
                mng.Update(role, EntityTypes.Role);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}