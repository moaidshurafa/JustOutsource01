using JustOutsource.DataAccess.Respiratory;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using JustOutsource.Models.ViewModels;
using JustOutsource.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace JustOutsource.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.Role_Job)]

    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        public JobController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IEmailSender emailService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Job> jobs = _unitOfWork.Job.GetAll(includeProperties: "Category")
                .Where(u => u.UserId == userId)
                .ToList();

            return View(jobs);
        }
        public IActionResult Upsert(int? id)
        {
            JobVM jobVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                }),
                Job = new Job()
            };
            if (id == 0 || id == null)
            {
                //create
                return View(jobVM);
            }
            else
            {
                var jobFromDb = _unitOfWork.Job.Get(u => u.Id == id);

                if (jobFromDb == null || jobFromDb.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return RedirectToAction("Index");
                }
                jobVM.Job = _unitOfWork.Job.Get(u => u.Id == id);
                return View(jobVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(JobVM jobVM, IFormFile? AdditionalFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (AdditionalFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(AdditionalFile.FileName);
                    string filePath = Path.Combine(wwwRootPath, @"Files\AdditionalFiles");
                    if (!string.IsNullOrEmpty(jobVM.Job.AdditionalFile))
                    {
                        var oldCvPath =
                            Path.Combine(wwwRootPath, jobVM.Job.AdditionalFile.TrimStart('\\'));
                        if (System.IO.File.Exists(oldCvPath))
                        {
                            System.IO.File.Delete(oldCvPath);
                        }
                    }
                    using (var cvStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        AdditionalFile.CopyTo(cvStream);
                    }
                    jobVM.Job.AdditionalFile = @"\Files\AdditionalFiles\" + fileName;
                }
                jobVM.Job.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (jobVM.Job.Id == 0)
                {
                    _unitOfWork.Job.Add(jobVM.Job);
                }
                else
                {
                    var existingJob = _unitOfWork.Job.Get(u => u.Id == jobVM.Job.Id);
                    if (existingJob == null || existingJob.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                    {
                        return RedirectToAction("Index");
                    }
                    _unitOfWork.Job.Update(jobVM.Job);
                }
                _unitOfWork.Save();
                TempData["success"] = "Job posted successfully";
                return RedirectToAction("Index");
            }
            else
            {
                jobVM.CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                });
                return View(jobVM);

            }
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Job? jobFromDb = _unitOfWork.Job.Get(u => u.Id == id);


            if (jobFromDb == null || jobFromDb.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index");
            }
            return View(jobFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Job? obj = _unitOfWork.Job.Get(u => u.Id == id);

            if (obj == null || obj.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index");
            }

            _unitOfWork.Job.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Job post deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult FindFreelancers()
        {
            IEnumerable<Freelancer> freelancerList = _unitOfWork.Freelancer.GetAll(includeProperties: "Category");
            return View(freelancerList);
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

        

    }



}


