using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class OrdersProductsController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<OrdersProducts>(EntityTypes.OrdersProducts);
            foreach (var order in data) textMod.AdaptObject(order, EntityTypes.OrdersProducts, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveAllByOrder(int orderId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<OrdersProducts>();
            var data = mng.RetrieveAll<OrdersProducts>(EntityTypes.OrdersProducts);
            foreach (var obj in data)
                if (obj.OrderId == orderId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) textMod.AdaptObject(resp, EntityTypes.OrdersProducts, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult Retrieve(int id) {
            try {
                var mng = new MasterManager();
                var resp = mng.Retrieve<OrdersProducts>(new OrdersProducts { OrdersProductsId = id }, EntityTypes.OrdersProducts);
                textMod.AdaptObject(resp, EntityTypes.OrdersProducts, false);
                apiResp = new ApiResponse() { Data = resp };
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(OrdersProducts order) {
            try {
                var mng = new MasterManager();
                if (order.OrdersProductsId == 0)
                    order.OrdersProductsId = mng.GetMaxId(order, EntityTypes.OrdersProducts) + 1;
                textMod.AdaptObject(order, EntityTypes.OrdersProducts, true);
                mng.Create<OrdersProducts>(order, EntityTypes.OrdersProducts);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}