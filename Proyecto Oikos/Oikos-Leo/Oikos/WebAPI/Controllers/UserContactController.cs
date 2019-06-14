using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class UserContactsController : ApiController
    {
        private ApiResponse apiResp;
        MasterManager mng = new MasterManager();

        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<UserContacts>(EntityTypes.UserContacts);
            return Ok(apiResp);
        }

        [HttpPost]
        public IHttpActionResult Retrieve(UserContacts userContacts)
        {
            try
            {
                var mng = new MasterManager();
                userContacts = mng.Retrieve<UserContacts>(userContacts, EntityTypes.UserContacts);
                apiResp = new ApiResponse() { Data = userContacts };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(UserContacts userContacts)
        {
            try
            {
                var mng = new MasterManager();
                if (userContacts.UserContactId == 0)
                    userContacts.UserContactId = mng.GetMaxId(userContacts, EntityTypes.UserContacts) + 1;   
                mng.Create<UserContactsController>(userContacts, EntityTypes.UserContacts);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        public IHttpActionResult Update(UserContacts userContacts)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(userContacts, EntityTypes.UserContacts);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(UserContacts userContacts)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(userContacts, EntityTypes.UserContacts);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}
