using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Exceptions;
using Stripe;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class PaymentController : ApiController
    {
        ApiResponse apiResp;
        // GET: Payment
        [HttpPost]
        public IHttpActionResult Charge(Token respToken)
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_egVUpLvvoAJEtXGhNoGNPLo3");

                // Token is created using Checkout or Elements!
                // Get the payment token submitted by the form:
                var token = respToken.Id; // Using ASP.NET MVC

                var options = new ChargeCreateOptions
                {
                    Amount = 999,
                    Currency = "usd",
                    Description = "Example charge",
                    SourceId = token,
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                apiResp = new ApiResponse();
                apiResp.Message = "Action was completed";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}