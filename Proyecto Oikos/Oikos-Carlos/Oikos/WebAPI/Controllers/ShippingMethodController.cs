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
    public class ShippingMethodController : ApiController
    {
        private ApiResponse apiResp;
        /*
         *This method is in charge of retrieving all the Shipping Methods registered in the database.
         *
         * @author Carlos Rios
         * @return The HttpMessage result of the action performed by method.
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<ShippingMethods>(EntityTypes.ShippingMethods);
            return Ok(apiResp);
        }

        [HttpPost]
        /*
         *This method is in charge of retrieving a Shipping method registered in the database.
         *
         * @author Carlos Rios
         * @param ShippingMethod prod - An instance of the Shipping class with the id to search for.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Retrieve(ShippingMethods prod)
        {
            try
            {
                var mng = new MasterManager();
                prod = mng.Retrieve<ShippingMethods>(prod, EntityTypes.ShippingMethods);
                apiResp = new ApiResponse() { Data = prod };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        /*
      *This method is in charge of registering a new shipping method in the database.
      *
      * @author Carlos Rios
      * @param ShippingMethod prod - An instance of the Shipping method class with the data of the attributes of Shipping Method to be registered in the data base
      * @return The HttpMessage result of the action performed by method.
      */

        [HttpPost]      
        public IHttpActionResult Create(ShippingMethods prod)
        {
            try
            {
                var mng = new MasterManager();
                prod.ShippingMethodId = mng.GetNextId(prod, EntityTypes.ShippingMethods);
                mng.Create<ShippingMethodController>(prod, EntityTypes.ShippingMethods);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        /*
         *This method is in charge of updating the information of a registered shipping method in the database.
         *
         * @author Carlos Rios
         * @param ShippingMethod prod - An instance of the Shipping Method class with the information to be updated.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Update(ShippingMethods prod)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(prod, EntityTypes.ShippingMethods);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        /*
        *This method is in charge of deleting a Shipping Method registered in the database.
        *
        * @author Carlos Rios
        * @param ShippingMethods prod - An instance of the Shipping Method class with the id to be deleted.
        * @return The HttpMessage result of the action performed by method.
        */
        public IHttpActionResult Delete(ShippingMethods prod)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(prod, EntityTypes.ShippingMethods);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
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
            var distance = Math.Sqrt(Math.Pow((userCoords[0] - center[0]), 2) + Math.Pow((userCoords[1] - center[1]), 2));
            if (radiusKm < distance)
                inArea = false;
            else
                inArea = true;

            return inArea;            
        }

        private List<ShippingMethods> GetShippingMethodsForShippingProvider(int shipProviderId)
        {

            var mng = new MasterManager();
            var lstShipMethods = mng.RetrieveAll<ShippingMethods>(EntityTypes.ShippingMethods);
            var shipProviderShipMethods = new List<ShippingMethods>();
            foreach (var s in lstShipMethods)
            {
                if (s.ShippingProviderId == shipProviderId)
                    shipProviderShipMethods.Add(s);
            }

            return shipProviderShipMethods;
        }

        [HttpGet]
        public IHttpActionResult GetShippingMethodsForShippingProvidersInArea(int userId, int storeId)
        {
            try
            {
                var mng = new MasterManager();
                var lstShipProviders = mng.RetrieveAll<ShippingProvider>(EntityTypes.ShippingProvider);
                var lstShipMethods = new List<ShippingMethods>();
                foreach(var s in lstShipProviders)
                {
                    if (IsInShippingProvAreaAndWarehouseRange(userId, storeId, s.ShippingProviderId))
                        lstShipMethods.AddRange(GetShippingMethodsForShippingProvider(s.ShippingProviderId));
                }

                apiResp = new ApiResponse();
                apiResp.Data = lstShipMethods;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        

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
            var lstUserLocations = mng.RetrieveAll<UserLocation>(EntityTypes.UserLocation);

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

                double[] spCenter = { shipProvider.AreaLatitude, shipProvider.AreaLongitude };
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

                double[] spCenter = { shiProvider.AreaLatitude, shiProvider.AreaLongitude };
                var userCoords = GetUserLocation(userId);

                return IsInArea(spCenter, userCoords, shiProvider.AreaRadius);
            }
            catch (BusinessException bex)
            {
                return false;
            }
        }

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
