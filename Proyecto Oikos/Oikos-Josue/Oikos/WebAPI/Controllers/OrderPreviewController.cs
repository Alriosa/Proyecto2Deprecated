using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class OrderPreviewController : ApiController {
        private ApiResponse apiResp;
        private readonly TextModule textMod = new TextModule();

        [HttpPost]
        public IHttpActionResult Create(List<OrderPreview> ordersList) {
            try {
                var mng = new MasterManager();
                foreach (var preview in ordersList) {
                    textMod.AdaptObject(preview.Order, EntityTypes.Orders, true);
                    if (preview.Order.OrderId == 0)
                        preview.Order.OrderId = mng.GetMaxId(preview.Order, EntityTypes.Orders) + 1;
                    mng.Create<Orders>(preview.Order, EntityTypes.Orders);
                    preview.Order.OrderId = mng.GetMaxId(preview.Order, EntityTypes.Orders);
                    foreach (var prod in preview.Products) {
                        prod.OrderId = preview.Order.OrderId;
                        if (prod.OrdersProductsId == 0)
                            prod.OrdersProductsId = mng.GetMaxId(prod, EntityTypes.OrdersProducts) + 1;
                        mng.Create<OrdersProducts>(prod, EntityTypes.OrdersProducts);
                        prod.OrdersProductsId = mng.GetMaxId(prod, EntityTypes.OrdersProducts);
                        var inventory = RetrieveProductUnits(prod.ProductId);
                        for (var i = 0; i < prod.Quantity; i++) {
                            mng.Create<OrdersProductsInventory>(new OrdersProductsInventory {
                                OrdersProductsInventoryId = mng.GetMaxId(new OrdersProductsInventory(), EntityTypes.OrdersProductsInventory) + 1,
                                OrdersProductsId = prod.OrdersProductsId,
                                InventoryId = inventory[i].InventoryId
                            }, EntityTypes.OrdersProductsInventory);
                            inventory[i].IsSold = true;
                            mng.Update(inventory[i], EntityTypes.Inventory);
                        }
                    }

                    mng.Create<OrdersStatus>(
                        new OrdersStatus {
                            OrdersStatusId = mng.GetMaxId(new OrdersStatus(), EntityTypes.OrdersStatus) + 1,
                            OrderId = preview.Order.OrderId,
                            StatusCode = 6,
                            StatusChangeDatetime = DateTime.Now
                        }, EntityTypes.OrdersStatus);
                }

                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            } catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        private List<Inventory> RetrieveProductUnits(int id) {
            var mng = new MasterManager();
            var lstInventory = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
            var data = new List<Inventory>();
            foreach (var unit in lstInventory)
                if (unit.ProductId == id && !unit.IsSold)
                    data.Add(unit);
            return data;
        }
    }
}