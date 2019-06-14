using EntitiesPOJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using CoreAPI;
using Exceptions;
using WebAPI.Models;
using System.Web.UI;

namespace WebAPI.Controllers{
    public class LogInController : ApiController{
        private ApiResponse apiResp;

        /**
             * This method logs an user in the platform
             * @author Tremi
             * @param pUser - User object 
             * @return returns an api response with the credentials
        */
        //POST api/login/LogIn
        [HttpPost]
        public IHttpActionResult LogIn(User pUser){
            try{
                apiResp = new ApiResponse();

                var mng = new MasterManager();
                var pwModule = new PasswordModule();


                var lstUsers = mng.RetrieveAll<User>(EntityTypes.Users);
                User foundUser = new User();

                pUser.Password = pwModule.EncryptPassword(pUser.Password);

                foreach (var user in lstUsers){
                    if (user.Email.Equals(pUser.Email) && user.Password.Equals(pUser.Password)){
                        foundUser = user;
                    }
                }

                Credentials usrCredentials = new Credentials(foundUser.UserId, foundUser.Name, foundUser.Email,
                    foundUser.UserStatusCode, foundUser.CurrencyCode);

                usrCredentials.RolesViewsList = null;
                    

                var lstUsersStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                if (null != lstUsersStores){
                    foreach (var obj in lstUsersStores){
                        if (obj.Owner == foundUser.UserId){
                            usrCredentials.StoreId = obj.StoreId;
                        }                                              
                    }
                }
                
                var lstUserProvs = mng.RetrieveAll<ShippingProvider>(EntityTypes.ShippingProvider);

                if (null != lstUserProvs) {
                    foreach (var obj in lstUserProvs) {
                        if (obj.Owner == foundUser.UserId) {
                            usrCredentials.ShippingProviderId = obj.ShippingProviderId;
                        }                       
                    }
                }
                

                apiResp.Data = usrCredentials;

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }
    }
}