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
    public class UserMediaController : ApiController
    {
        ApiResponse apiResp;

        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<UserMedia>(EntityTypes.UserMedia);
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveById(int id)
        {
            try
            {
                var mng = new MasterManager();
                var userMedia = new UserMedia
                {
                    UsersMediaId = id
                };

                userMedia = mng.Retrieve<UserMedia>(userMedia, EntityTypes.UserMedia);

                apiResp = new ApiResponse();
                apiResp.Data = userMedia;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(UserMedia userMedia)
        {
            try {
                var mng = new MasterManager();
                if (userMedia.UsersMediaId == 0)
                    userMedia.UsersMediaId = mng.GetMaxId(userMedia, EntityTypes.UserMedia) + 1;

                mng.Create<UserMedia>(userMedia, EntityTypes.UserMedia);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        public IHttpActionResult Update(UserMedia userMedia)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(userMedia, EntityTypes.UserMedia);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(UserMedia userMedia)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(userMedia, EntityTypes.UserMedia);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        public IHttpActionResult Suspend(Store store)
        {
            apiResp = new ApiResponse();

            try
            {
                var mng = new MasterManager();
                var lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores)
                {
                    if (store.Identification == s.Identification)
                    {
                        s.StoreStatusCode = "STS02";
                        mng.Update(s, EntityTypes.Store);
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}