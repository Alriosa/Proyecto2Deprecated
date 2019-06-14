using CoreAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers{
    [ExceptionFilter]
    public class InventoryController : ApiController{
        ApiResponse apiResp = new ApiResponse();

        /**
            * This method creates inventory units
            * @author Tremi
            * @param No params
            * @return api response with the success or error message
         */
        //POST api/Inventory/AddStock
        [HttpPost]
        public IHttpActionResult AddStock(ProviderOrder pProductProvOrder) {
            try{
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                pProductProvOrder.IsSold = false;

                for (int i = 0; i < pProductProvOrder.Quantity; i++){
                    Inventory newInventoryUnit = new Inventory(pProductProvOrder.ProductId,
                        pProductProvOrder.PriceBought, pProductProvOrder.PurchaseDatetime,
                        pProductProvOrder.SerialNumber, pProductProvOrder.IsSold);
                    newInventoryUnit.InventoryId = mng.GetNextId(newInventoryUnit, EntityTypes.Inventory);
                    mng.Create<Inventory>(newInventoryUnit, EntityTypes.Inventory);
                }

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }


        /**
        * This method creates inventory units
            * @author Tremi
            * @param pStoreId - String with storeid from credentials
        * @return api response with a list of the invetory of the store
            */
        //GET api/Inventory/RetrieveStoreInventory?pStoreId={store id}
        [HttpGet]
        public IHttpActionResult RetrieveStoreInventory(string pStoreId){
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var lstInventory = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
            var lstProduct = mng.RetrieveAll<Product>(EntityTypes.Product);
            List<Inventory> data = new List<Inventory>();
            foreach (var product in lstProduct){
                if (product.StoreId == int.Parse(pStoreId)){
                    foreach (var unit in lstInventory){
                        if (unit.ProductId == product.ProductId && unit.IsSold is false) {
                            data.Add(unit);
                        }
                    }
                }
            }

            apiResp.Data = data;

            return Ok(apiResp);
        }

        /**
        * This method retrieves units of a product
            * @author Tremi
            * @param pProductId - String with product id from product list
        * @return api response with a list of the units of a product
            */
        //GET api/Inventory/RetrieveStoreInventory?pProductId={product id}
        [HttpGet]
        public IHttpActionResult RetrieveProductUnits(string pProductId){
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var lstInventory = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
            List<Inventory> data = new List<Inventory>();
            foreach (var unit in lstInventory){
                if (unit.ProductId == int.Parse(pProductId) && unit.IsSold == false){
                    data.Add(unit);
                }
            }

            apiResp.Data = data;
            return Ok(apiResp);
        }

        /**
        * This method retrieves the unit count for a product
        * @author Erick Garro
        * @param pProductId - String with product id from product list
        * @return api response with a list of the units of a product
    */
        //GET api/Inventory/RetrieveProductUnitsCount?productId={product id}
        [HttpGet]
        public IHttpActionResult RetrieveProductUnitsCount(int productId) {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            var lstInventory = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
            List<Inventory> data = new List<Inventory>();
            var count = 0;
            foreach (var unit in lstInventory) {
                if (unit.ProductId == productId && unit.IsSold == false) {
                    count++;
                }
            }
            apiResp.Data = count;
            return Ok(apiResp);
        }

        /**
                    * This method updates a unit in the inventory
                    * @author Tremi
                    * @param pInv - Inventory object
                    * @returns apiResponse with action confirmed
         */
        //PUT api/inventory/Update
        [HttpPut]
        public IHttpActionResult Update(Inventory pInv){
            try{
                var mng = new MasterManager();


                mng.Update(pInv, EntityTypes.Inventory);

                apiResp = new ApiResponse();
                apiResp.Message = "Inventory was updated.";

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
            * This method  Deletes an unit in the inventory
            * @author Tremi
            * @param pInv - Inventory object
            * @returns apiResponse with action confirmed
        */
        //DELETE api/inventory/Delete
        [HttpDelete]
        public IHttpActionResult Delete(Inventory pInv){
            try{
                var mng = new MasterManager();


                mng.Delete(pInv, EntityTypes.Inventory);

                apiResp = new ApiResponse{
                    Message = "User deleted."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex){
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /**
            * This method retrieves a inventory 
            * @author Tremi
            * @param pInv Inventory object
            * @returns apiResponse with inventory requested 
        */
        //Post api/inventory/Retrieve
        [HttpPost]
        public IHttpActionResult Retrieve(Inventory pInv) {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();
                
                apiResp.Data = mng.Retrieve<Inventory>(pInv,EntityTypes.Inventory);

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }
        /**
            * This method retrieves a inventory count of product
            * @author Tremi
            * @param pProductId  int
            * @returns apiResponse with inventory count for product
        */
        //Post api/inventory/RetrieveProductInventoryCount?pProductId={product id}
        [HttpGet]
        public IHttpActionResult RetrieveProductInventoryCount(string pProductId) {
            try {
                apiResp = new ApiResponse();
                var mng = new MasterManager();

                List<Inventory> lstInv = mng.RetrieveAll<Inventory>(EntityTypes.Inventory);
                int count = 0;

                foreach (var inv in lstInv){
                    if (inv.ProductId == Int32.Parse(pProductId)&&inv.IsSold is false) count++;
                }

                apiResp.Data = count;

                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage));
            }
        }
    }
}