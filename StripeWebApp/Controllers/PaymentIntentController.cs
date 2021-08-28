using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripeWebApp.Controllers
{
    public class PaymentIntentController : Controller
    {
        // GET: PaymentIntent
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PayNow(PaymentIntentCreateRequest request)
        {
            StripeConfiguration.ApiKey = "sk_test_51JTVOgLmAflnHCBtV8yFeLUvQ8a0917t83JeyEdPqbOnRE0xXt67We9IklyUBC6NCfUXLe5PN7hc0QJLK4Frcd7V009iAihs7D";
            var paymentIntents = new PaymentIntentService();
            var paymentIntent = paymentIntents.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "usd",
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }

        public class Item
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }
    }
}