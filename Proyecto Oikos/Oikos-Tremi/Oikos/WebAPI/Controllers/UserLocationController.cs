using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using WebAPI.Models;

namespace WebAPI.Controllers {
    [ExceptionFilter]
    public class UserLocationController : ApiController {

        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();

        [HttpPost]
        // GET POST api/UserLocation/Create
        /*
         * This method registers a new shipping address in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(UserLocation userLocation) {

            try {
                userLocation.UserLocationsId = mng.GetNextId(userLocation, EntityTypes.UserLocation);
                mng.Create<UserLocation>(userLocation, EntityTypes.UserLocation);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/UserLocation/RetrieveAll
        /*
         *This method retrieves all the shipping addresses registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            List<UserLocation> lstActiveUserLocations = new List<UserLocation>();

            try
            {
                var lstUserLocationDB = mng.RetrieveAll<UserLocation>(EntityTypes.UserLocation);

                foreach (var ul in lstUserLocationDB){
                    if (ul.IsActive) {
                        lstActiveUserLocations.Add(ul);
                    }
                }
                apiResp.Data = lstActiveUserLocations;
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/UserLocation/Retrieve
        /*
         *This method retrieves an specific shipping address by ID registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Retrieve(UserLocation userLocation) {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.Retrieve<UserLocation>(userLocation, EntityTypes.UserLocation);
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/UserLocation/RetrieveByCustomerId
        /*
         *This method retrieves an specific shipping address by the customer ID registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveByIdentification(UserLocation userLocation) {
            apiResp = new ApiResponse();

            try {
                List<UserLocation> lstUserLocations = mng.RetrieveAll<UserLocation>(EntityTypes.UserLocation);

                foreach (var s in lstUserLocations) {
                    if (userLocation.UserId == s.UserId) {
                        apiResp.Data = s;
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        // PUT api/UserLocation/Update
        /*
         *This method updates an existing shipping address in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Update(UserLocation userLocation) {
            try {
                var mng = new MasterManager();
                mng.Update(userLocation, EntityTypes.UserLocation);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }


        [HttpDelete]
        // DELETE api/UserLocation/Delete
        /*
         *This method performs a soft delete on a shipping address registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Delete(UserLocation userLocation) {
            apiResp = new ApiResponse();

            try {
                List<UserLocation> lstUserLocations = mng.RetrieveAll<UserLocation>(EntityTypes.UserLocation);

                foreach (var s in lstUserLocations) {
                    if (userLocation.UserLocationsId == s.UserLocationsId && userLocation.UserId == s.UserId) {
                        s.IsActive = false;
                        mng.Update(s, EntityTypes.UserLocation);
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);

            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}