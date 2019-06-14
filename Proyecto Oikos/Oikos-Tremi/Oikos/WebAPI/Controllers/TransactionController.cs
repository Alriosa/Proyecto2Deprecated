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
    [ExceptionFilter]
    public class TransactionController : ApiController
    {
        ApiResponse apiResp;

        /*
         * This method retrieves all Transactions stored in
         * the database.
         *
         * @author: Josué Quirós
         *
         * @return: object with the information of all transactions
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Transaction>(EntityTypes.Transaction);

            return Ok(apiResp);
        }

        /*
         * This method retrieves
         * the Transaction with requested id
         *
         * @author: Josué Quirós
         *
         * @param id: id number of the transaction in database
         *
         * @return: object with the information of the transaction
         */
        [HttpGet]
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

        /*
         * This method creates a new transaction and sends its information
         * to the database.
         *
         * @author: Josué Quirós
         *
         * @param transaction: a Transaction object with the information to be added
         *
         * @return: a response object with a message showing creation was completed successfully
         */
        [HttpPost]
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

        /*
         * This method updates the information of an existing transaction
         * in the database
         *
         * @author: Josué Quirós
         *
         * @param transaction: a Transaction object with the information to be updated
         *
         * @return: a response object with a message showing the action was completed successfully
         */
        [HttpPut]
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

        /*
         * This method deletes an existing transaction from the database
         *
         * @author: Josué Quirós
         *
         * @param transaction: a Transaction object with the info to be removed
         *
         * @return: a response object showing that the action was completed successfully
         */
        [HttpDelete]
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