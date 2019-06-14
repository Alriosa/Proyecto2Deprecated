using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class InternalAccountController : ApiController
    {

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
        public IHttpActionResult Create(InternalAccount internalAccount)
        {            
            try
            {
                internalAccount.InternalAccountId = mng.GetNextId(internalAccount, EntityTypes.InternalAccount);              

                var dbCsType = mng.Retrieve<InternalAccount>(internalAccount, EntityTypes.InternalAccount);                

                mng.Create<InternalAccount>(internalAccount, EntityTypes.InternalAccount);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
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
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();

            try
            {
                apiResp.Data = mng.RetrieveAll<InternalAccount>(EntityTypes.InternalAccount);
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
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
        public IHttpActionResult Retrieve(InternalAccount internalAccount)
        {
            apiResp = new ApiResponse();

            try
            {
                apiResp.Data = mng.Retrieve<InternalAccount>(internalAccount, EntityTypes.CustomerServiceRequest);
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
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
        public IHttpActionResult Update(InternalAccount internalAccount)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(internalAccount, EntityTypes.InternalAccount);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
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
        public IHttpActionResult Resolve(InternalAccount internalAccount)
        {
            try
            {
                var mng = new MasterManager();                
                mng.Update(internalAccount, EntityTypes.InternalAccount);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
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
        public IHttpActionResult Delete(InternalAccount internalAccount)
        {
            apiResp = new ApiResponse();            

            try
            {
                var mng = new MasterManager();
                mng.Delete(internalAccount,EntityTypes.InternalAccount);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}


