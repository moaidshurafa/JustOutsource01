using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using JustOutsource.Models.ViewModels;
using JustOutsource.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace JustOutsource.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = SD.Role_Job)]

    public class JobController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public JobController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                var jobFromDb = _unitOfWork.Job.Get(u=>u.Id == id);

                if(jobFromDb == null || jobFromDb.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return RedirectToAction("Index");
                }
                jobVM.Job = _unitOfWork.Job.Get(u=>u.Id==id);
                return View(jobVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(JobVM jobVM, IFormFile? AdditionalFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(AdditionalFile != null)
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
    }
}
