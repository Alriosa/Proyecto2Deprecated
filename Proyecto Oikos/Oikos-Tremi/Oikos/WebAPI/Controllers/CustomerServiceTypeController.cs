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
    public class CustomerServiceTypeController : ApiController {

        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();

        [HttpPost]
        // GET POST /customerServiceType/Create
        /*
         *This method creates a new customer service type in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(CustomerServiceType customerServiceType) {

            try {
                customerServiceType.CustomerServiceTypeId = mng.GetNextId(customerServiceType, EntityTypes.CustomerServiceType);
                mng.Create<CustomerServiceType>(customerServiceType, EntityTypes.CustomerServiceType);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/customerServiceType/RetrieveAll
        /*
         *This method retrieves all the customer service types registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.RetrieveAll<CustomerServiceType>(EntityTypes.CustomerServiceType);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/customerServiceType/Retrieve
        /*
         *This method retrieves a customer service type registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Retrieve(CustomerServiceType customerServiceType) {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.Retrieve<CustomerServiceType>(customerServiceType, EntityTypes.CustomerServiceType);
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        

        [HttpPut]
        // PUT api/customerServiceType/Update
        /*
         *This method updates an existing customer service type in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Update(CustomerServiceType customerServiceType) {
            try {
                var mng = new MasterManager();
                mng.Update(customerServiceType, EntityTypes.CustomerServiceType);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }


        [HttpDelete]
        // DELETE api/customerServiceType/Delete
        /*
         *This method performs a soft delete of an existing customer service type in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Delete(CustomerServiceType customerServiceType) {
            apiResp = new ApiResponse();
            CustomerServiceType newCst = new CustomerServiceType();

            try {
                var mng = new MasterManager();
                newCst = mng.Retrieve<CustomerServiceType>(customerServiceType, EntityTypes.CustomerServiceType);
                newCst.IsActive = false;
                mng.Update(newCst, EntityTypes.CustomerServiceType);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }

            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
   }
}
