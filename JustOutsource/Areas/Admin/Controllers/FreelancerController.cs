using Humanizer;
using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using JustOutsource.Models.ViewModels;
using JustOutsource.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.Security.Claims;



namespace JustOutsource.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Freelancer)]

    public class FreelancerController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FreelancerController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Freelancer> freelancers = _unitOfWork.Freelancer.GetAll(includeProperties: "Category")
                .Where(u => u.UserId == userId)  // Filter by the current user's UserId
                .ToList();

            return View(freelancers);
        }
        public IActionResult Upsert(int? id)
        {
            FreelancerVM freelancerVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                }),
                Freelancer = new Freelancer()
            };
            if (id == 0 || id == null)
            {
                //create
                return View(freelancerVM);
            }

            else
            {
                //update
                // Update existing freelancer
                var freelancerFromDb = _unitOfWork.Freelancer.Get(u => u.Id == id);

                // Check if the freelancer belongs to the current user
                if (freelancerFromDb == null || freelancerFromDb.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    // If not, return NotFound
                    return RedirectToAction("Index");
                }
                freelancerVM.Freelancer = _unitOfWork.Freelancer.Get(u => u.Id == id);
                return View(freelancerVM);

            }
        }
        [HttpPost]
        public IActionResult Upsert(FreelancerVM freelancerVM, IFormFile? file, IFormFile? cvFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string freelancerPath = Path.Combine(wwwRootPath, @"Images\Freelancer");

                    if (!string.IsNullOrEmpty(freelancerVM.Freelancer.ImageUrl))
                    {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, freelancerVM.Freelancer.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(freelancerPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    freelancerVM.Freelancer.ImageUrl = @"\Images\Freelancer\" + fileName;

                }
                if (cvFile != null)
                {
                    string cvFileName = Guid.NewGuid().ToString() + Path.GetExtension(cvFile.FileName);
                    string cvPath = Path.Combine(wwwRootPath, @"Files\CVs");
                    if (!string.IsNullOrEmpty(freelancerVM.Freelancer.CV))
                    {
                        var oldCvPath =
                            Path.Combine(wwwRootPath, freelancerVM.Freelancer.CV.TrimStart('\\'));
                        if (System.IO.File.Exists(oldCvPath))
                        {
                            System.IO.File.Delete(oldCvPath);
                        }
                    }
                    using (var cvStream = new FileStream(Path.Combine(cvPath, cvFileName), FileMode.Create))
                    {
                        cvFile.CopyTo(cvStream);
                    }
                    freelancerVM.Freelancer.CV = @"\Files\CVs\" + cvFileName;
                }

                // Set the UserId to the current logged-in user's UserId
                freelancerVM.Freelancer.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (freelancerVM.Freelancer.Id == 0)
                {
                    _unitOfWork.Freelancer.Add(freelancerVM.Freelancer);
                }
                else
                {
                    var existingFreelancer = _unitOfWork.Freelancer.Get(u => u.Id == freelancerVM.Freelancer.Id);
                    if (existingFreelancer == null || existingFreelancer.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                    {
                        return RedirectToAction("Index"); // Prevent updating freelancers that don't belong to the current user
                    }
                    _unitOfWork.Freelancer.Update(freelancerVM.Freelancer);
                }
                _unitOfWork.Save();
                TempData["success"] = "Freelancer created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                freelancerVM.CategoryList = _unitOfWork.Category.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                });
                return View(freelancerVM);

            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Freelancer? freelancerFromDb = _unitOfWork.Freelancer.Get(u => u.Id == id);


            if (freelancerFromDb == null || freelancerFromDb.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index");
            }
            return View(freelancerFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Freelancer? obj = _unitOfWork.Freelancer.Get(u => u.Id == id);

            if (obj == null || obj.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index");
            }

            _unitOfWork.Freelancer.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Freelancer deleted successfully";
            return RedirectToAction("Index");
        }
        public IActionResult FindJob()
        {
            IEnumerable<Job> JobList = _unitOfWork.Job.GetAll(includeProperties: "Category");
            return View(JobList);
        }
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Fetch the job with its related category details
            var jobFromDb = _unitOfWork.Job.Get(u => u.Id == id, includeProperties: "Category");

            if (jobFromDb == null)
            {
                return NotFound();
            }

            return View(jobFromDb);
        }
        
    }
}
