// CustomerServiceRequestController

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
    public class CustomerServiceRequestController : ApiController {

        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();

        [HttpPost]
        // GET POST /customerServiceRequest/Create
        /*
         *This method registers a new customer service requests in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(CustomerServiceRequest customerServiceRequest) {
            var csType = new CustomerServiceType(customerServiceRequest.CustomerServiceRequestId);
            csType.CustomerServiceTypeId = customerServiceRequest.CustomerServiceTypeId;
            try
            {
                customerServiceRequest.CustomerServiceRequestId = mng.GetNextId(customerServiceRequest, EntityTypes.CustomerServiceRequest);
                customerServiceRequest.IsResolved = false;
                customerServiceRequest.IsActive = true;
                customerServiceRequest.RequestDatetime = DateTime.Now;

                var dbCsType = mng.Retrieve<CustomerServiceType>(csType, EntityTypes.CustomerServiceType);

                if (dbCsType.AcceptsRefunds == true) {
                    customerServiceRequest.IsResolved = true;
                }

                mng.Create<CustomerServiceRequest>(customerServiceRequest, EntityTypes.CustomerServiceRequest);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/customerServiceRequest/RetrieveAll
        /*
         *This method retrieves all the customer service requests registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.RetrieveAll<CustomerServiceRequest>(EntityTypes.CustomerServiceRequest);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/customerServiceRequest/Retrieve
        /*
         *This method retrieves a customer service requests registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Retrieve(CustomerServiceRequest customerServiceRequest) {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.Retrieve<CustomerServiceRequest>(customerServiceRequest, EntityTypes.CustomerServiceRequest);
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        [HttpPut]
        // PUT api/customerServiceRequest/Update
        /*
         *This method updates an existing customer service requests in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Update(CustomerServiceRequest customerServiceRequest) {
            try {
                var mng = new MasterManager();
                mng.Update(customerServiceRequest, EntityTypes.CustomerServiceRequest);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpPut]
        // PUT api/customerServiceRequest/Resolve
        /*
         *This method updates a customer service requests to resolved in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Resolve(CustomerServiceRequest customerServiceRequest) {
            try {
                var mng = new MasterManager();
                customerServiceRequest.IsResolved = true;
                mng.Update(customerServiceRequest, EntityTypes.CustomerServiceRequest);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpDelete]
        // DELETE api/customerServiceRequest/Delete
        /*
         *This method performs a soft delete of an existing customer service registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Delete(CustomerServiceRequest customerServiceRequest) {
            apiResp = new ApiResponse();
            CustomerServiceRequest newCsr = new CustomerServiceRequest();

            try {
                var mng = new MasterManager();
                newCsr = mng.Retrieve<CustomerServiceRequest>(customerServiceRequest, EntityTypes.CustomerServiceRequest);
                newCsr.IsActive = false;
                mng.Update(newCsr, EntityTypes.CustomerServiceRequest);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}

