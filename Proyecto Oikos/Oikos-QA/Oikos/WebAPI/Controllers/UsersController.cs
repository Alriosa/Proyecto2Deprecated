using CoreAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class UsersController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [HttpGet]
        public IHttpActionResult RetrieveAllUsers(){

            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<User>(EntityTypes.Users);

            return Ok(apiResp);
        }
        [HttpPost]
        public IHttpActionResult CreateUser(User pUser){
            try
            {
                var mng = new MasterManager();
                mng.Create<User>(pUser,EntityTypes.Users);

                apiResp = new ApiResponse{
                    Message = "User was created."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        //PUT api/currency/UpdateCurrency
        [HttpPut]
        public IHttpActionResult UpdateUser(User pUser)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(pUser, EntityTypes.Users);

                apiResp = new ApiResponse();
                apiResp.Message = "User was updated.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        //DELETE api/currency/DeleteCurrency
        [HttpDelete]
        public IHttpActionResult DeleteUser(User pUser)
        {
            try
            {
                var mng = new MasterManager();

                mng.Delete(pUser, EntityTypes.Users);

                apiResp = new ApiResponse
                {
                    Message = "User deleted."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}