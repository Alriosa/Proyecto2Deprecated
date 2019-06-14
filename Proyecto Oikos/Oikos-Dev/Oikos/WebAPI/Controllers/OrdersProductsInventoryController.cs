using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class OrdersProductsInventoryController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<OrdersProductsInventory>(EntityTypes.OrdersProductsInventory);
            foreach (var order in data) textMod.AdaptObject(order, EntityTypes.OrdersProductsInventory, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveAllByProductId(int productId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<OrdersProductsInventory>();
            var data = mng.RetrieveAll<OrdersProductsInventory>(EntityTypes.OrdersProductsInventory);
            foreach (var obj in data)
                if (obj.OrdersProductsId == productId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) textMod.AdaptObject(resp, EntityTypes.OrdersProductsInventory, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult Retrieve(int id) {
            try {
                var mng = new MasterManager();
                var resp = mng.Retrieve<OrdersProductsInventory>(new OrdersProductsInventory { OrdersProductsInventoryId = id }, EntityTypes.OrdersProductsInventory);
                textMod.AdaptObject(resp, EntityTypes.OrdersProductsInventory, false);
                apiResp = new ApiResponse() { Data = resp };
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(OrdersProductsInventory order) {
            try {
                var mng = new MasterManager();
                if (order.OrdersProductsInventoryId == 0)
                    order.OrdersProductsInventoryId = mng.GetMaxId(order, EntityTypes.OrdersProductsInventory) + 1;
                textMod.AdaptObject(order, EntityTypes.OrdersProductsInventory, true);
                mng.Create<OrdersProductsInventory>(order, EntityTypes.OrdersProductsInventory);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}