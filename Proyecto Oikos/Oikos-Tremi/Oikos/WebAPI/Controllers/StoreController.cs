using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Antlr.Runtime.Misc;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [ExceptionFilter]
    public class StoreController : ApiController {

        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();
        StoreManager smMng = new StoreManager();

        [HttpPost]
        // GET POST /store/Create
        /*
         *This method registers a new store in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(Store store) {
            Store newStore = new Store();
            try
            {
                newStore = smMng.Create(store);
                mng.Create<Store>(newStore, EntityTypes.Store);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/store/RetrieveAll
        /*
         *This method retrieves all the stores registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            List<Store> filteredStoreList= new ListStack<Store>();

            try {
                var lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);
                foreach (var s in lstStores) {
                    if (s.StoreStatusCode != "STS00") {
                        filteredStoreList.Add(s);
                        apiResp.Message = "Action was executed.";
                    }
                }

                apiResp.Data = filteredStoreList;
                apiResp.Message = "OK";
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/store/Retrieve
        /*
         *This method retrieves an specific store by ID registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Retrieve(Store store) {
            apiResp = new ApiResponse();

            try {
                apiResp.Data = mng.Retrieve<Store>(store, EntityTypes.Store);
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/store/Retrieve?=storeId={store Id}
        /*
         *This method retrieves an specific store by ID registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Retrieve(int storeId) {
            apiResp = new ApiResponse();
            Store store = new Store();
            store.StoreId = storeId;
            try {
                apiResp.Data = mng.Retrieve<Store>(store, EntityTypes.Store);
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // POST api/store/RetrieveByIdentification
        /*
         *This method retrieves an specific store by identification number registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveByIdentification(Store store) {
            apiResp = new ApiResponse();

            try {
                List<Store>  lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores) {
                    if (store.Identification == s.Identification) {
                        apiResp.Data = s;
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        // GET api/store/RetrieveByOwnerId?ownerId=ownerId
        /*
         *This method retrieves an specific store by identification number registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult RetrieveByOwnerId(int ownerId) {
            apiResp = new ApiResponse();

            try {
                List<Store> lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores) {
                    if (ownerId == s.Owner) {
                        apiResp.Data = s;
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        // PUT api/store/Update
        /*
         *This method updates an existing store in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Update(Store store) {
            try {
                var mng = new MasterManager();
                mng.Update(store, EntityTypes.Store);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }


        [HttpDelete]
        // DELETE api/store/Delete
        /*
         *This method performs a soft delete on a store registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Delete(Store store) {
            apiResp = new ApiResponse();
            
            try {
                List<Store> lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores) {
                    if (store.Identification == s.Identification)
                    {
                        s.StoreStatusCode = "STS00";
                        mng.Update(s, EntityTypes.Store);
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);

            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        // PUT api/store/Suspend
        /*
         *This method performs suspends a store registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Suspend(Store store) {
            apiResp = new ApiResponse();

            try {
                List<Store> lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores) {
                    if (store.Identification == s.Identification) {
                        s.StoreStatusCode = "STS02";
                        mng.Update(s, EntityTypes.Store);
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);

            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        // PUT api/store/UpdateCommission
        /*
         *This method updates the commission charged to a store registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult UpdateCommission(Store store) {
            apiResp = new ApiResponse();

            try {
                List<Store> lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores) {
                    if (store.Identification == s.Identification) {
                        s.Commission = store.Commission;
                        mng.Update(s, EntityTypes.Store);
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);

            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        /*
         *This method performs an updated of the latitude and longitude coordinates for a store's warehouse registered in the database.
         *
         * @author Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        // PUT api/store/UpdateWarehouse
        public IHttpActionResult UpdateWarehouse(Store store) {
            apiResp = new ApiResponse();

            try {
                List<Store> lstStores = mng.RetrieveAll<Store>(EntityTypes.Store);

                foreach (var s in lstStores) {
                    if (store.Identification == s.Identification) {
                        s.WarehouseLatitude = store.WarehouseLatitude;
                        s.WarehouseLongitude = store.WarehouseLongitude;
                        mng.Update(s, EntityTypes.Store);
                        apiResp.Message = "Action was executed.";
                    }
                }
                return Ok(apiResp);

            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


    }
}