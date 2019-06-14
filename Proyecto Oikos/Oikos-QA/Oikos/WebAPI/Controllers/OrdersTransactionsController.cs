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
    public class OrdersTransactionsController : ApiController
    {
        ApiResponse apiResp;
        // GET api/<controller>
        public IHttpActionResult RetrieveAll()
        {
            var mng = new MasterManager();
            apiResp = new ApiResponse();
            apiResp.Data = mng.RetrieveAll<OrdersTransactions>(EntityTypes.OrdersTransactions);
            return Ok(apiResp);
        }

        // GET api/<controller>/5
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

        // POST api/<controller>
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

        // PUT api/<controller>/5
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

        // DELETE api/<controller>/5
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