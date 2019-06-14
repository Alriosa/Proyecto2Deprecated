using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class OrdersController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpGet]
        public IHttpActionResult RetrieveOrderDetailByStore(int storeId) {
            var orderDetailLst = new List<OrderDetail>();
            var detail = new OrderDetail();
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<Orders>();
            var data = mng.RetrieveAll<Orders>(EntityTypes.Orders);
            foreach (var obj in data)
                if (obj.StoreId == storeId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) {
                textMod.AdaptObject(resp, EntityTypes.Orders, false);
                var user = mng.Retrieve<User>(new User { UserId=resp.UserId}, EntityTypes.Users);
                detail.UserName = user.Name;
                detail.UserEmail = user.Email;
                detail.ShippMethod = mng.Retrieve<ShippingMethods>(
                    new ShippingMethods {ShippingMethodId = resp.ShippingMethodId}, EntityTypes.ShippingMethods);
                detail.OrderDatetime = resp.OrderDatetime;
                detail.Destination = mng.Retrieve<UserLocation>(new UserLocation{UserLocationsId = resp.Destination}, EntityTypes.UserLocation);
                var lst = mng.RetrieveAll<OrdersProducts>(EntityTypes.OrdersProducts);
                foreach (var obj in lst)
                    if (obj.OrderId == resp.OrderId)
                        detail.Products.Add(obj);
                foreach (var prod in detail.Products) textMod.AdaptObject(prod, EntityTypes.OrdersProducts, false);
                orderDetailLst.Add(detail);
                var status = mng.RetrieveAll<OrdersStatus>(EntityTypes.OrdersStatus);
                foreach (var obj in status)
                    if (obj.OrderId == resp.OrderId && obj.IsActive) {
                        detail.Status = obj;
                        break;
                    }
            }

            apiResp.Message = "Action was executed.";
            apiResp.Data = orderDetailLst;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var data = mng.RetrieveAll<Orders>(EntityTypes.Orders);
            foreach (var order in data) textMod.AdaptObject(order, EntityTypes.Orders, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult RetrieveAllByUser(int userId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var filteredLst = new List<Orders>();
            var data = mng.RetrieveAll<Orders>(EntityTypes.Orders);
            foreach (var obj in data)
                if (obj.UserId == userId)
                    filteredLst.Add(obj);
            foreach (var resp in filteredLst) textMod.AdaptObject(resp, EntityTypes.Orders, false);
            apiResp.Data = data;
            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult Retrieve(int id) {
            try {
                var mng = new MasterManager();
                var resp = mng.Retrieve<Orders>(new Orders {OrderId = id}, EntityTypes.Orders);
                textMod.AdaptObject(resp, EntityTypes.Orders, false);
                apiResp = new ApiResponse() {Data = resp};
                return Ok(apiResp);
            } catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult Create(Orders order) {
            try {
                var mng = new MasterManager();
                if (order.OrderId == 0)
                    order.OrderId = mng.GetMaxId(order, EntityTypes.Orders) + 1;
                textMod.AdaptObject(order, EntityTypes.Orders, true);
                mng.Create<ProductRequestResponses>(order, EntityTypes.Orders);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }
    }
}