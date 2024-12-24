using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace JustOutsource.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Freelancer> freelancerList = _unitOfWork.Freelancer.GetAll(includeProperties:"Category");
            return View(freelancerList);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Details(int freelancerId)
        {
            ShoppingCart cart = new()
            {
                Freelancer = _unitOfWork.Freelancer.Get(u => u.Id == freelancerId, includeProperties: "Category"),
                FreelancerId = freelancerId
            };
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.FreelancerId == shoppingCart.FreelancerId);

            if (cartFromDb == null)
            {
                // shopping cart exists
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                TempData["success"] = "Freelancer is added successfully to your cart.";

            }
            else
            {
                // add cart record
                TempData["success"] = "This freelancer is already in your cart.";


            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
