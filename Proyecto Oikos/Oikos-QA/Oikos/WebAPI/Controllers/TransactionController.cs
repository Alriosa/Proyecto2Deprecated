using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using Microsoft.Ajax.Utilities;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TransactionController : ApiController
    {
        ApiResponse apiResp;
        // GET api/<controller>
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Transaction>(EntityTypes.Transaction);

            return Ok(apiResp);
        }

        // GET api/<controller>/5
        public IHttpActionResult RetrieveById(int id)
        {
            try
            {
                var mng = new MasterManager();
                var transaction = new Transaction
                {
                    TransactionId = id
                };

                transaction = mng.Retrieve<Transaction>(transaction, EntityTypes.Transaction);

                apiResp = new ApiResponse();
                apiResp.Data = transaction;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // POST api/<controller>
        public IHttpActionResult Create(Transaction transaction)
        {
            try
            {
                var mng = new MasterManager();
                mng.Create<Transaction>(transaction, EntityTypes.Transaction);

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
        public IHttpActionResult Update(Transaction transaction)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(transaction, EntityTypes.Transaction);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" +  bex.AppMessage.Message));
            }
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(Transaction transaction)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(transaction, EntityTypes.Transaction);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}