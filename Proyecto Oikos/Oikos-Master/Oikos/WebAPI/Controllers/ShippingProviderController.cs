using System;
using System.Web.Http;
using System.Collections.Generic;
using CoreAPI;
using WebAPI.Models;
using EntitiesPOJO;
using Exceptions;
using Microsoft.Ajax.Utilities;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class ShippingProviderController : ApiController
    {
        ApiResponse apiResp;

        /*
         * This method retrieves all Shipping Providers stored in
         * the database.
         *
         * @author: Josué Quirós
         *
         * @return: object with the information of all shipping providers
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<ShippingProvider>(EntityTypes.ShippingProvider);
            return Ok(apiResp);
        }

        /*
         * This method retrieves
         * the Shipping Provider with requested id
         *
         * @author: Josué Quirós
         *
         * @param id: id number of the shipping provider in database
         *
         * @return: object with the information of the shipping provider
         */
        [HttpGet]
        public IHttpActionResult RetrieveById(int id)
        {
            try
            {
                var mng = new MasterManager();
                var shipProvider = new ShippingProvider
                {
                    ShippingProviderId = id
                };

                shipProvider = mng.Retrieve<ShippingProvider>(shipProvider, EntityTypes.ShippingProvider);

                apiResp = new ApiResponse();
                apiResp.Data = shipProvider;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method creates a new Shipping provider and sends its information
         * to the database.
         *
         * @author: Josué Quirós
         *
         * @param shipProvider: a ShippingProvider object with the information to be added
         *
         * @return: a response object with a message showing creation was completed successfully
         */
        [HttpPost]
        public IHttpActionResult Create(ShippingProvider shipProvider)
        {
            try
            {
                var mng = new MasterManager();
                if (shipProvider.ShippingProviderId == 0)
                    shipProvider.ShippingProviderId = mng.GetMaxId(shipProvider, EntityTypes.ShippingProvider) + 1;
                
                mng.Create<ShippingProvider>(shipProvider, EntityTypes.ShippingProvider);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method updates the information of an existing shipping provider
         * in the database
         *
         * @author: Josué Quirós
         *
         * @param shipProvider: a ShippingProvider object with the information to be updated
         *
         * @return: a response object with a message showing the action was completed successfully
         */
        [HttpPut]
        public IHttpActionResult Update(ShippingProvider shipProvider)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(shipProvider, EntityTypes.ShippingProvider);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method deletes an existing shipping provider from the database
         *
         * @author: Josué Quirós
         *
         * @param shipProvider: a ShippingProvider object with the info to be removed
         *
         * @return: a response object showing that the action was completed successfully
         */
        [HttpDelete]
        public IHttpActionResult Delete(ShippingProvider shipProvider)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(shipProvider, EntityTypes.ShippingProvider);

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
        // PUT api/store/Suspend
        /*
         *This method performs suspends a store registered in the database.
         *
         * @author Josué Quirós
         * 
         * @return the IHttpActionResult result of the action performed by method.
         */
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

        /*
         * This method retrieves a shipping provider based on the user who created it
         *
         * @author: Josué Quirós
         *
         * @param ownerId: The user's Id.
         *
         * @return: A server response with the shipping providers that belong to the user
         */
        [HttpGet]
        public IHttpActionResult RetrieveByOwnerId(int ownerId)
        {
            

            try
            {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                List<ShippingProvider> lstStores = mng.RetrieveAll<ShippingProvider>(EntityTypes.ShippingProvider);

                foreach (var s in lstStores)
                {
                    if (ownerId == s.Owner)
                    {
                        apiResp.Data = s;
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

        /*
         * This method retrieves all Shipping providers that are in the same area as the user
         *
         * @author: Josué Quirós
         *
         * @param userId: The user's Id number in the database.
         *
         * @return: A server response with a list of shipping providers that are in the same area as the user
         */

        
        [HttpGet]
        public IHttpActionResult GetAllShippingProvidersInArea(int userId, string type)
        {
            try
            {
                var mng = new MasterManager();
                List<ShippingProvider> shipProviders = mng.RetrieveAll<ShippingProvider>(EntityTypes.ShippingProvider);
                List<ShippingProvider> shipProvInArea = new List<ShippingProvider>();

                foreach (var sp in shipProviders)
                {
                    if (type == "client")
                    {
                        if (CheckIfUserIsInArea(sp.ShippingProviderId, userId))
                            shipProvInArea.Add(sp);
                    }
                    else
                    {
                        if (CheckIfWarehouseIsInArea(sp.ShippingProviderId, userId))
                        {
                            shipProvInArea.Add(sp);
                        }
                    }

                }

                apiResp = new ApiResponse();
                apiResp.Data = shipProvInArea;

                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));                
            }
        }

        /*
         * This method checks whether the user is whithin the area of a given shipping provider
         *
         * @author: Josué Quirós
         *
         * @param shipProviderId: Id number of shipping provider to check.
         *
         * @param userId: Id number of the user to check
         *
         * @return: boolean value, true if user is in area.
         */

        private bool CheckIfWarehouseIsInArea(int shipProviderId, int userId)
        {
            try
            {
                var mng = new MasterManager();
                var shipProvider = new ShippingProvider
                {
                    ShippingProviderId = shipProviderId
                };

                shipProvider = mng.Retrieve<ShippingProvider>(shipProvider, EntityTypes.ShippingProvider);

                double[] spCenter = {shipProvider.AreaLatitude, shipProvider.AreaLongitude};
                var warehouseCoords = GetWarehouseLocation(userId);

                return IsInArea(spCenter, warehouseCoords, shipProvider.AreaRadius);
            }
            catch (BusinessException bex)
            {
                return false;
            }
        }

        private bool CheckIfUserIsInArea(int shipProviderId, int userId)
        {
            try
            {
                var mng = new MasterManager();
                var shiProvider = new ShippingProvider
                {
                    ShippingProviderId = shipProviderId
                };

                shiProvider = mng.Retrieve<ShippingProvider>(shiProvider, EntityTypes.ShippingProvider);

                double[] spCenter = {shiProvider.AreaLatitude, shiProvider.AreaLongitude };
                var userCoords = GetUserLocation(userId);

                return IsInArea(spCenter, userCoords, shiProvider.AreaRadius);
            }
            catch (BusinessException bex)
            {
                return false;
            }
        }

        /*
         * This method gets the user location in latitude /longitude coordinates
         *
         * @author: Josué Quirós
         *
         * @param userId: Id of the user to get the location from.
         *
         * @return: an array of two double representing latitude and longitude respectively.
         */

        private double[] GetWarehouseLocation(int userId)
        {
            var warehouseCoords = new double[2];
            var mng = new MasterManager();
            var lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

            foreach (var s in lstStores)
            {
                if (s.StoreId == userId)
                {
                    warehouseCoords[0] = s.WarehouseLatitude;
                    warehouseCoords[1] = s.WarehouseLongitude;
                }
            }
            return warehouseCoords;
        }

        private double[] GetUserLocation(int userId)
        {
            double[] userCoords = new double[2];
            var mng = new MasterManager();
            List<UserLocation> lstUserLocations = mng.RetrieveAll<UserLocation>(EntityTypes.UserLocation);

            foreach (var s in lstUserLocations)
            {
                if (s.UserId == userId)
                {
                    userCoords[0] = s.Latitude;
                    userCoords[1] = s.Longitude;
                }
            }

            return userCoords;
        }

        /*
         * This method checks if a coordinate is within the area of a circle, which radius is given in meters
         *
         * @author: Josué Quirós
         *
         * @param center: An array of double representing the circle coordinates at it's center
         *
         * @param userCoords: An array of double representing the user's location coordinates
         *
         * @param radius: A double representing the radius of the circle, given in meters
         *
         * @return: A boolean, true if user coordinates are within given radius.
         */
        private bool IsInArea(double[] center, double[] userCoords, double radius)
        {
            bool inArea;
           var radiusKm = radius / 1000;
            var distance = Math.Sqrt(Math.Pow((userCoords[0] - center[0]),2) + Math.Pow((userCoords[1] - center[1]),2));
            if (radiusKm < distance)
                inArea = false;
            else
                inArea = true;
            
            return inArea;
;        }

        private bool IsInShippingProvAreaAndWarehouseRange(int userId, int storeId, int shipProviderId)
        {
            if (CheckIfUserIsInArea(shipProviderId, userId) && CheckIfWarehouseIsInArea(shipProviderId, storeId))
            {
                return true;
            }

            return false;
        }

    }
}