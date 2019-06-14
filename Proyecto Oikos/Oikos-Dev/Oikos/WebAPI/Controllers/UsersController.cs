using CoreAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers{
    [ExceptionFilter]
    public class UsersController : ApiController{
        ApiResponse apiResp = new ApiResponse();

        /**
            * This method retrieves all users registered in the database as an api response
            * @author Tremi
            * @param No params
            * @return returns an api response with a list of all the users in the system
         */
        //GET api/users/RetrieveAll
        [HttpGet]
        public IHttpActionResult RetrieveAll(){
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<User>(EntityTypes.Users);

            return Ok(apiResp);
        }

        /**
            * This method retrieves a user by it's identification
            * @author Tremi
            * @param User object
            * @returns apiResponse with user requested 
        */
        //Post api/users/Retrieve
        [HttpPost]
        public IHttpActionResult Retrieve(User pUser){
            try{
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                User foundUser = new User();

                foreach (var obj in mng.RetrieveAll<User>(EntityTypes.Users)){
                    if (pUser.Email.Equals(obj.Email)) { foundUser = obj; }
                    
                }
                
                apiResp.Data = foundUser;

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }

        /**
           * This method creates a user with all its data
           * @author Tremi
           * @param pUser - User object
           * @returns apiResponse with user requested 
       */
        //POST api/users/Create
        [HttpPost]
        public IHttpActionResult Create(User pUser){
            try{
                var mng = new MasterManager();
                var pwModule = new PasswordModule();

                pUser.UserId = mng.GetMaxId(pUser, EntityTypes.Users)+1;

                pUser.Password = pwModule.EncryptPassword(pUser.Password);

                mng.Create<User>(pUser, EntityTypes.Users);

                apiResp = new ApiResponse{
                    Message = "User was created."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
           * This method creates a user with random password
           * @author Tremi
           * @param pUser - User object
           * @returns apiResponse with user requested 
       */
        //POST api/users/CreateWithPassword
        [HttpPost]
        public IHttpActionResult CreateWithPassWord(User pUser) {
            try {
                var mng = new MasterManager();
                var pwModule = new PasswordModule();
                pUser.UserId = mng.GetMaxId(pUser, EntityTypes.Users)+1;
                pUser.Password = pwModule.EncryptPassword(pwModule.PasswordGenerator());

                mng.Create<User>(pUser, EntityTypes.Users);

                apiResp = new ApiResponse {
                    Message = "User was created."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        /**
           * This method updates an user information
           * @author Tremi
           * @param pUser - User object
           * @returns apiResponse with action confirmed
       */
        //PUT api/users/Update
        [HttpPut]
        public IHttpActionResult Update(User pUser){
            try{
                var mng = new MasterManager();
                var pwModule = new PasswordModule();

                pUser.Password = pwModule.EncryptPassword(pUser.Password);

                mng.Update(pUser, EntityTypes.Users);

                apiResp = new ApiResponse();
                apiResp.Message = "User was updated.";

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
            * This method soft Deletes an User
            * @author Tremi
            * @param pUser - User object
            * @returns apiResponse with action confirmed
        */
        //DELETE api/users/Delete
        [HttpDelete]
        public IHttpActionResult Delete(User pUser){
            try{
                var mng = new MasterManager();

                pUser.UserStatusCode = "0";

                mng.Update(pUser, EntityTypes.Users);

                apiResp = new ApiResponse{
                    Message = "User deleted."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        public IHttpActionResult GetLastId(){
            try{
                
                var mng = new MasterManager();
                apiResp = new ApiResponse();

                apiResp.Data = mng.GetMaxId(new User(), EntityTypes.Users);

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        
    }
}