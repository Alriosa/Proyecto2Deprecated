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
    public class PaymentMethodController : ApiController
    {
        ApiResponse apiResp;

        /*
         * This method retrieves all Payment Methods stored in
         * the database.
         *
         * @author: Josué Quirós
         *
         * @return: response object with the information of all payment methods
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<PaymentMethod>(EntityTypes.PaymentMethod);

            return Ok(apiResp);
        }

        /*
         * This method retrieves
         * the Payment Method with requested id
         *
         * @author: Josué Quirós
         *
         * @param id: id number of the payment method in database
         *
         * @return: object with the information of the payment method
         */
        [HttpGet]
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

        /*
         * This method creates a new Payment Method and sends its information
         * to the database.
         *
         * @author: Josué Quirós
         *
         * @param payMethod: a PaymentMethod object with the information to be added
         *
         * @return: a response object with a message showing creation was completed successfully
         */
        [HttpPost]
        public IHttpActionResult Create(PaymentMethod payMethod)
        {
            try
            {
                var mng = new MasterManager();
                if (payMethod.PaymentMethodId == 0)
                    payMethod.PaymentMethodId = mng.GetMaxId(payMethod, EntityTypes.PaymentMethod) + 1;

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

        /*
         * This method updates the information of an existing payment method
         * in the database
         *
         * @author: Josué Quirós
         *
         * @param payMethod: a PaymentMethod object with the information to be updated
         *
         * @return: a response object with a message showing the action was completed successfully
         */
        [HttpPut]
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

        /*
         * This method deletes an existing payment method from the database
         *
         * @author: Josué Quirós
         *
         * @param payMethod: a PaymentMethod object with the info to be removed
         *
         * @return: a response object showing that the action was completed successfully
         */
        [HttpDelete]
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