using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class OptionListController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();
        MasterManager mng = new MasterManager();
        OptionListManager opMng = new OptionListManager();
        public System.Collections.Specialized.NameValueCollection QueryString { get; }

        [HttpPost]
        // GET POST api/OptionList/Create
        /*
         *This method creates a new option for a list in the database.
         *
         * @author Erick Garro
         * @param Optionlist option
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Create(OptionList option)
        {

            try
            {
                option.ListId = option.ListId.ToUpper();
                mng.Create<OptionList>(option, EntityTypes.OptionList);
                apiResp = new ApiResponse() {Message = "Action was executed."};
                return Ok(apiResp);
            }
            catch (BusinessException e)
            {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }


        // GET api/OptionList/RetrieveAll
        /*
         *This method retrieves all the options for a specific list registered in the database.
         *
         * @author Dennis Córdoba modified by Erick Garro
         * @return the IHttpActionResult result of the action performed by method.
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll(string id) {
            try {
                var mng = new OptionListManager();
                var option = new OptionList {
                    ListId = id.ToUpper()
                };

                var lstOptions = mng.RetrieveByListId(option);
                return Ok(lstOptions);
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // POST api/OptionList/Retrieve
        /*
         * This method retrieves a specific option registered in the database.
         *
         * @author Erick Garro
         * @param Optionlist option
         * @return the IHttpActionResult result of the action performed by method.
         */
        [HttpPost]
        public IHttpActionResult Retrieve(OptionList option) {
            option.ListId = option.ListId.ToUpper();
            option.Value = option.Value.ToUpper();

            try {
                var mng = new OptionListManager();
                option.ListId.ToUpper();
                option.Value.ToUpper();

                return Ok(mng.Retrieve(option));
            }
            catch (BusinessException bex) {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        // GET POST api/OptionList/Update
        /*
         *This method udpates an existing OptionList list in the database.
         *
         * @author Erick Garro
         * @param Optionlist option
         * @return the IHttpActionResult result of the action performed by method.
         */
        public IHttpActionResult Update(OptionList option) {

            try {
                option.ListId = option.ListId.ToUpper();
                mng.Create<OptionList>(option, EntityTypes.OptionList);
                apiResp = new ApiResponse() { Message = "Action was executed." };
                return Ok(apiResp);
            }
            catch (BusinessException e) {
                return InternalServerError(new Exception(e.ExceptionId + "-" + e.AppMessage.Message));
            }
        }


//        /**
//        * This method deletes a currency
//        * @author Tremi modified by Erick Garro
//        * @param Optionlist option
//        * @return returns an api response confirming the action
//         */
//        //DELETE api/OptionList/Delete
//        [HttpDelete]
//        public IHttpActionResult Delete(OptionList option) {
//            try {
//                var mng = new MasterManager();
//
//                mng.Delete(option, EntityTypes.OptionList);
//
//                apiResp = new ApiResponse {
//                    Message = "Option deleted from list."
//                };
//
//                return Ok(apiResp);
//            }
//            catch (BusinessException bex) {
//                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
//            }
//        }
    }
}

