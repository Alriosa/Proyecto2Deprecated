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
    public class OrdersTransactionsController : ApiController
    {
        ApiResponse apiResp;

        /*
         * Author: Josué Quirós
         * This method retrieves all Orders Transactions stored in
         * the database.
         *
         * @return: object with the information of all Orders Transactions
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            var mng = new MasterManager();
            apiResp = new ApiResponse();
            apiResp.Data = mng.RetrieveAll<OrdersTransactions>(EntityTypes.OrdersTransactions);
            return Ok(apiResp);
        }

        /*
         * Author: Josué Quirós
         * This method retrieves
         * the Order Transaction with requested id
         *
         * @param id: id number of the order transaction in database
         *
         * @return: object with the information of the order transaction
         */
        [HttpGet]
        public IHttpActionResult RetrieveById(int id)
        {
            try
            {
                var mng = new MasterManager();
                var ordersT = new OrdersTransactions
                {
                    OrderTransactionsId = id
                };
                ordersT = mng.Retrieve<OrdersTransactions>(ordersT, EntityTypes.OrdersTransactions);
                apiResp = new ApiResponse();
                apiResp.Data = ordersT;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * Author: Josué Quirós
         * This method creates a new Order Transaction and sends its information
         * to the database.
         *
         * @param oTrans: a OrdersTransaction object with the information to be added
         *
         * @return: a response object with a message showing creation was completed successfully
         */
        [HttpPost]
        public IHttpActionResult Create(OrdersTransactions oTrans)
        {
            try
            {
                var mng = new MasterManager();
                mng.Create<OrdersTransactions>(oTrans, EntityTypes.OrdersTransactions);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * Author: Josué Quirós
         * This method updates the information of an existing order transaction
         * in the database
         *
         * @param oTrans: a OrdersTransactions object with the information to be updated
         *
         * @return: a response object with a message showing the action was completed successfully
         */
        [HttpPut]
        public IHttpActionResult Update(OrdersTransactions oTrans)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(oTrans, EntityTypes.OrdersTransactions);

                apiResp = new ApiResponse();
                apiResp.Message = "Action Completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * Author: Josué Quirós
         * This method deletes an existing order transaction from the database
         *
         * @param oTrans: a OrdersTransactions object with the info to be removed
         *
         * @return: a response object showing that the action was completed successfully
         */
        [HttpDelete]
        public IHttpActionResult Delete(OrdersTransactions oTrans)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(oTrans, EntityTypes.OrdersTransactions);

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