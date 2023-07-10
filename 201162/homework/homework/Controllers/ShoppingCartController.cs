using eBoxOffice.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace homework.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        public IActionResult Index()
        {
            var model = _shoppingCartService.GetShoppingCartInfo(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            return View(model);
        }

        public IActionResult DeleteFromShoppingCart(int id)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.DeleteProductFromShoppingCart(loggedInUser, id);

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult PayNow(string stripeEmail, string stripeToken)
        {
            var customerService = new CustomerService();
            var chargeService = new ChargeService();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var order = _shoppingCartService.GetShoppingCartInfo(userId);


            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "eBoxOffice Payment",
                Currency = "usd",
                Customer = customer.Id
            });

            if(charge.Status == "succeeded") {
                var result = this.Order();
                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
            }

            return RedirectToAction("Index", "ShoppingCart");

        }

        private Boolean Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.OrderNow(userId);

            return result;
        }
    }
}
