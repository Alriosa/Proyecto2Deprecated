using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class OrdersStatusController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<OrdersStatus>(EntityTypes.OrdersStatus);
            foreach (var order in data) textMod.AdaptObject(order, EntityTypes.OrdersStatus, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveAllByOrder(int orderId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<OrdersStatus>();
            var data = mng.RetrieveAll<OrdersStatus>(EntityTypes.OrdersStatus);
            foreach (var obj in data)
                if (obj.OrderId == orderId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) textMod.AdaptObject(resp, EntityTypes.OrdersStatus, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveActiveByOrder(int orderId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<OrdersStatus>(EntityTypes.OrdersStatus);
            foreach (var obj in data)
                if (obj.OrderId == orderId && obj.IsActive)
                    apiResp.Data = obj;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult Retrieve(int id) {
            try {
                var mng = new MasterManager();
                var resp = mng.Retrieve<OrdersStatus>(new OrdersStatus {OrdersStatusId = id}, EntityTypes.OrdersStatus);
                textMod.AdaptObject(resp, EntityTypes.OrdersStatus, false);
                apiResp = new ApiResponse() {Data = resp};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(OrdersStatus order) {
            try {
                var mng = new MasterManager();
                if (order.OrderId == 0)
                    order.OrderId = mng.GetMaxId(order, EntityTypes.OrdersStatus) + 1;
                textMod.AdaptObject(order, EntityTypes.OrdersStatus, true);
                mng.Create<OrdersStatus>(order, EntityTypes.OrdersStatus);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}