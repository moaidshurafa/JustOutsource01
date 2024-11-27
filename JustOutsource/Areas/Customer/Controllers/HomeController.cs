using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public IActionResult Details(int freelancerId)
        {
            Freelancer freelancer = _unitOfWork.Freelancer.Get(u=>u.Id== freelancerId, includeProperties: "Category");
            return View(freelancer);
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
