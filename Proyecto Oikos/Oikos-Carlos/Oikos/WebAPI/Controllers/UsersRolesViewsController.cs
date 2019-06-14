using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class UsersRolesViewsController : ApiController {
        ApiResponse apiResp;

        [HttpPost]
        /*
         *This method is in charge of registering a new role in the database.
         *
         * @author Leonardo Mora
         * @param UserRoleView urv - An instance of the Role class with the information of the role to be registered.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Create(UserRoleView urv) {
            try {
                var mng = new MasterManager();
                if (urv.UsersRolesViewsId == 0) urv.UsersRolesViewsId = mng.GetMaxId(urv, EntityTypes.UserRoleView) + 1;
                urv.UserId = 0;
                mng.Create<UserRoleView>(urv, EntityTypes.UserRoleView);
                return Ok(new ApiResponse() {Message = "Action was executed."});
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        /*
         *This method is in charge of retrieving all the objects registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<UserRoleView>(EntityTypes.UserRoleView);
            return Ok(apiResp);
        }

        [HttpGet]
        /*
         *This method is in charge of retrieving all the objects registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAllByRole(int roleId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<UserRoleView>();
            var data = mng.RetrieveAll<UserRoleView>(EntityTypes.UserRoleView);
            foreach (var obj in data)
                if (obj.RoleId == roleId && obj.IsActive)
                    filteredLst.Add(obj);
            apiResp.Data = filteredLst;
            return Ok(apiResp);
        }

        [HttpPost]
        /*
         *This method is in charge of retrieving a the roles and views of an user registered in the database.
         *
         * @author Leonardo Mora
         * @param UserRoleView urv - An instance of the UserRoleView class with the id to search for.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Retrieve(UserRoleView urv) {
            try {
                var mng = new MasterManager();
                urv = mng.Retrieve<UserRoleView>(urv, EntityTypes.UserRoleView);
                apiResp = new ApiResponse() {Data = urv};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpDelete]
        /*
         *This method is in charge of deleting an object registered in the database.
         *
         * @author Leonardo Mora
         * @param UserRoleView urv - An instance of the UserRoleView class with the id to be deleted.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Delete(UserRoleView urv) {
            try {
                var mng = new MasterManager();
                urv.IsActive = false;
                mng.Update(urv, EntityTypes.UserRoleView);
                return Ok(new ApiResponse() {Message = "Action was executed."});
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}