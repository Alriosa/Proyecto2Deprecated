using System;
using System.Collections.Generic;
using System.Web.Http;
using CoreAPI;
using EntitiesPOJO;
using WebAPI.Models;
using Exceptions;
using Newtonsoft.Json;
namespace WebAPI.Controllers {
    public class ViewController : ApiController{
        ApiResponse apiResp;

        [HttpGet]
        /*
         *This method is in charge of retrieving the views available to the store.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAllStoreViews() {
            var mng = new MasterManager();
            var lst = mng.RetrieveAll<View>(EntityTypes.View);
            var storesLst = new List<View>();
            foreach (var view in lst) {
                if (view.Type == "STORE" || view.Type == "ADMINS") {
                    storesLst.Add(view);
                }
            }
            return Ok(new ApiResponse{Data = storesLst});
        }

        [HttpGet]
        /*
         *This method is in charge of retrieving all the categories registered in the database.
         *
         * @author Leonardo Mora
         * @return The HttpMessage result of the action performed by method.
         */
        public IHttpActionResult RetrieveAll() {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<View>(EntityTypes.View);
            return Ok(apiResp);
        }

    }
}