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
    public class CustomerController : ApiController
    {
        private ApiResponse apiResp;
        /*
         *This method is in charge of retrieving all the Customers registered in the database.
         *
         * @author Carlos Rios
         * @return The HttpMessage result of the action performed by method.
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Customer>(EntityTypes.Customer);
            return Ok(apiResp);
        }

        [HttpPost]
        /*
         *This method is in charge of retrieving a Customer registered in the database.
         *
         * @author Carlos Rios
         * @param Customer customer - An instance of the Customer class with the id to search for.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Retrieve(Customer customer)
        {
            try
            {
                var mng = new MasterManager();
                customer = mng.Retrieve<Customer>(customer, EntityTypes.Customer);
                apiResp = new ApiResponse() { Data = customer };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        /*
      *This method is in charge of registering a new Customer in the database.
      *
      * @author Carlos Rios
      * @param Customer customer - An instance of the Customer class with the data of the attributes of customer to be registered in the data base
      * @return The HttpMessage result of the action performed by method.
      */

        [HttpPost]
        public IHttpActionResult Create(Customer customer)
        {
            try
            {
                var mng = new MasterManager();
                customer.UserId = mng.GetNextId(customer, EntityTypes.Customer);
                mng.Create<CustomerController>(customer, EntityTypes.Customer);
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
         *This method is in charge of updating the information of a registered Customers in the database.
         *
         * @author Carlos Rios
         * @param Customer customer - An instance of the Customers class with the information to be updated.
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult Update(Customer customer)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(customer, EntityTypes.Customer);
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
        *This method is in charge of deleting a Customer registered in the database.
        *
        * @author Carlos Rios
        * @param Customer customer - An instance of the Customer class with the id to be deleted.
        * @return The HttpMessage result of the action performed by method.
        */
        public IHttpActionResult Delete(Customer customer)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(customer, EntityTypes.Customer);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}