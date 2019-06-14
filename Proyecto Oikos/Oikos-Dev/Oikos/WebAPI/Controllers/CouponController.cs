using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;
using CoreAPI;
using EntitiesPOJO;
using Exceptions;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class CouponController : ApiController
    {
        ApiResponse apiResp;
        /*         
         * This method retrieves all Coupons stored in
         * the database.
         *
         * @author: Josué Quirós
         *
         * @return: object with the information of all coupons
         */
        [HttpGet]
        public IHttpActionResult RetrieveAll()
        {
            apiResp = new ApiResponse();
            var mng = new MasterManager();
            apiResp.Data = mng.RetrieveAll<Coupon>(EntityTypes.Coupon);

            return Ok(apiResp);
        }

        /*
         * This method retrieves
         * the Coupon with requested id
         *
         * @author: Josué Quirós
         *
         * @param id: id number of the coupon in database
         *
         * @return: object with the information of the coupon
         */
        [HttpGet]
        public IHttpActionResult RetrieveById(int id)
        {

            try
            {
                var mng = new MasterManager();
                var coupon = new Coupon
                {
                    CouponId = id
                };

                coupon = mng.Retrieve<Coupon>(coupon, EntityTypes.Coupon);
                apiResp = new ApiResponse();
                apiResp.Data = coupon;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method creates a new coupon and sends its information
         * to the database.
         *
         * @uthor: Josué Quirós
         *
         * @param coupon: a Coupon object with the information to be added
         *
         * @return: a response object with a message showing creation was completed successfully
         */
        [HttpPost]
        public IHttpActionResult Create(Coupon coupon)
        {
            try
            {
                var mng = new MasterManager();
                if (coupon.CouponId == 0)
                    coupon.CouponId = mng.GetMaxId(coupon, EntityTypes.Coupon) + 1;

                mng.Create<Coupon>(coupon, EntityTypes.Coupon);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * 
         * This method updates the information of an existing coupon
         * in the database
         *
         * @author: Josué Quirós
         *
         * @param coupon: a Coupon object with the information to be updated
         *
         * @return: a response object with a message showing the action was completed successfully
         */
        [HttpPut]
        public IHttpActionResult Update(Coupon coupon)
        {
            try
            {
                var mng = new MasterManager();
                mng.Update(coupon, EntityTypes.Coupon);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*
         * This method deletes an existing coupon from the database
         *
         * @author: Josué Quirós
         *
         * @param coupon: a Coupon object with the info to be removed
         *
         * @return: a response object showing that the action was completed successfully
         */
        [HttpDelete]
        public IHttpActionResult Delete(Coupon coupon)
        {
            try
            {
                var mng = new MasterManager();
                mng.Delete(coupon, EntityTypes.Coupon);

                apiResp = new ApiResponse();
                apiResp.Message = "Action completed";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}