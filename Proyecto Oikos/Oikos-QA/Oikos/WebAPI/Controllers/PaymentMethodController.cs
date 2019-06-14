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
    public class PaymentMethodController : ApiController
    {
        ApiResponse apiResp;
        // GET api/<controller>
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<PaymentMethod>(EntityTypes.PaymentMethod);

            return Ok(apiResp);
        }

        // GET api/<controller>/5
        public IHttpActionResult RetrieveById(int id)
        {
            try
            {
                var mng = new MasterManager();
                var payMethod = new PaymentMethod
                {
                    PaymentMethodId = id
                };

                payMethod = mng.Retrieve<PaymentMethod>(payMethod, EntityTypes.PaymentMethod);
                apiResp = new ApiResponse();
                apiResp.Data = payMethod;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // POST api/<controller>
        public IHttpActionResult Create(PaymentMethod payMethod)
        {
            try
            {
                var mng = new MasterManager();
                mng.Create<PaymentMethod>(payMethod, EntityTypes.PaymentMethod);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Update(PaymentMethod payMethod)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(payMethod, EntityTypes.PaymentMethod);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(PaymentMethod payMethod)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(payMethod, EntityTypes.PaymentMethod);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}