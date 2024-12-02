using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using JustOutsource.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JustOutsource.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId==userId, includeProperties:"Freelancer")
            };

            foreach(var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnDeliveryTime(cart);
                ShoppingCartVM.OrderTotal += cart.Price;
            }

            return View(ShoppingCartVM);
        }
        private double GetPriceBasedOnDeliveryTime(ShoppingCart shoppingCart)
        {
            if (shoppingCart.CountDays >= 7)
            {
                return shoppingCart.Freelancer.ListPrice * 1.0;
            }
            else if (shoppingCart.CountDays > 4)
            {
                return shoppingCart.Freelancer.ListPrice * 1.2;
            }
            else if (shoppingCart.CountDays >= 3)
            {
                return shoppingCart.Freelancer.ListPrice * 1.5;
            }
            else
            {
                return shoppingCart.Freelancer.ListPrice * 1.8;
            }
        }
    }
}
