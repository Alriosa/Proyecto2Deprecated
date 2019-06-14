using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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

        // GET api/<controller>
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<ShippingProvider>(EntityTypes.ShippingProvider);
            return Ok(apiResp);
        }

        // GET api/<controller>/5
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

        // POST api/<controller>
        public IHttpActionResult Create(ShippingProvider shipProvider)
        {
            try
            {
                var mng = new MasterManager();
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

        // PUT api/<controller>/5
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

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(ShippingProvider shipProvider)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(shipProvider, EntityTypes.ShippingProvider);

                apiResp = new ApiResponse();
                apiResp.Message = "Áction Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}