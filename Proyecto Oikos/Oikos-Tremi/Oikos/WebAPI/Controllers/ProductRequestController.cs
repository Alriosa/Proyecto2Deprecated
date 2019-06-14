using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class ProductRequestController : ApiController
    {
        ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        /*
         * This method retrieves all Product Requests stored in
         * the database.
         *
         * @author: Josué Quirós
         *
         * @return: object with the information of all product requests
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<ProductRequest>(EntityTypes.ProductRequest);
            return Ok(apiResp);
        }

        /*
         * This method retrieves
         * the Product Request with requested id
         *
         * @author: Josué Quirós
         *
         * @param id: id number of the product request in database
         *
         * @return: object with the information of the product request
         */
        [HttpGet]
        public IHttpActionResult RetrieveById(int id)
        {
            try
            {
                var mng = new MasterManager();
                var prodRequest = new ProductRequest
                {
                    ProductRequestId = id
                };

                prodRequest = mng.Retrieve<ProductRequest>(prodRequest, EntityTypes.ProductRequest);
                apiResp = new ApiResponse();
                apiResp.Data = prodRequest;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
            
        }

        /*
         * This method creates a new Product Request and sends its information
         * to the database.
         *
         * @author: Josué Quirós
         *
         * @param prodRequest: a ProductRequest object with the information to be added
         *
         * @return: a response object with a message showing creation was completed successfully
         */        
        [HttpPost]
        public IHttpActionResult Create(ProductRequest prodRequest)
        {
            try
            {
                var mng = new MasterManager();
                if (prodRequest.ProductRequestId == 0)
                    prodRequest.ProductRequestId = mng.GetMaxId(prodRequest, EntityTypes.ProductRequest) + 1;

                mng.Create<ProductRequest>(prodRequest, EntityTypes.ProductRequest);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method updates the information of an existing product request
         * in the database
         *
         * @author: Josué Quirós
         *
         * @param prodRequest: a ProductRequest object with the information to be updated
         *
         * @return: a response object with a message showing the action was completed successfully
         */
        [HttpPut]
        public IHttpActionResult Update(ProductRequest prodRequest)
        {
            try
            {
                var mng = new MasterManager();
                textMod.AdaptObject(prodRequest, EntityTypes.ProductRequest, true);
                mng.Update(prodRequest, EntityTypes.ProductRequest);
                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method deletes an existing product request from the database
         *
         * @author: Josué Quirós
         *
         * @param prodRequest: a ProductRequest object with the info to be removed
         *
         * @return: a response object showing that the action was completed successfully
         */
        [HttpDelete]
        public IHttpActionResult Delete(ProductRequest prodRequest)
        {
            try
            {
                var mng = new MasterManager();
                prodRequest.IsActive = false;
                textMod.AdaptObject(prodRequest, EntityTypes.ProductRequest, true);
                mng.Update(prodRequest, EntityTypes.ProductRequest);
                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        private bool CheckIfUserIsInDeliveryArea(int shipProviderId, int userId)
        {
            try
            {
                var mng = new MasterManager();
                var shipProvider = new ShippingProvider
                {
                    ShippingProviderId = shipProviderId
                };

                shipProvider = mng.Retrieve<ShippingProvider>(shipProvider, EntityTypes.ShippingProvider);                     
                var userLocation = new UserLocation
                {
                    UserId = userId
                };

                userLocation = mng.Retrieve<UserLocation>(userLocation, EntityTypes.Users);

                double[] spCenter = {shipProvider.AreaLatitude, shipProvider.AreaLongitude};
                double[] userCoords = {userLocation.Latitude, userLocation.Longitude};
                return IsInArea(spCenter, userCoords, shipProvider.AreaRadius);
            }
            catch(BusinessException bex)
            {
                return false;
            }

        }

        public bool IsInArea(double[] center, double[] userCoordinates, double radius)
        {
            bool inArea;
            var radiusKm = radius / 1000;
            var distance = Math.Sqrt((Math.Pow((userCoordinates[0] - center[0]), 2) +
                                      Math.Pow((userCoordinates[1] - center[1]), 2)));
            inArea = false ? distance > radiusKm : inArea = true; 
            return inArea;

        }
    }
}