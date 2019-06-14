using CoreAPI;
using EntitiesPOJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class CurrencyController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [HttpGet]
        public IHttpActionResult RetrieveAllCurrencies()
        {

            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Currency>(EntityTypes.Currency);

            return Ok(apiResp);
        }
        [HttpPost]
        public IHttpActionResult CreateCurrency(Currency pCurrency)
        {
            try
            {
                var mng = new MasterManager();
                mng.Create<Currency>(pCurrency, EntityTypes.Currency);

                apiResp = new ApiResponse
                {
                    Message = "Currency was created."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        //PUT api/currency/UpdateCurrency
        [HttpPut]
        public IHttpActionResult UpdateCurrency(Currency pCurrency)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(pCurrency, EntityTypes.Currency);

                apiResp = new ApiResponse();
                apiResp.Message = "Currency was updated.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        //DELETE api/currency/DeleteCurrency
        [HttpDelete]
        public IHttpActionResult DeleteCurrency(Currency pCurrency)
        {
            try
            {
                var mng = new MasterManager();
                
                mng.Delete(pCurrency,EntityTypes.Currency);

                apiResp = new ApiResponse
                {
                    Message = "Currency deleted."
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}