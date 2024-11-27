using JustOutsource.DataAccess.Data;
using JustOutsource.DataAccess.Respiratory.IRespiratory;
using JustOutsource.Models;
using JustOutsource.Models.ViewModels;
using JustOutsource.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;



namespace JustOutsource.Areas.Admin.Controllers
{
    //   [Authorize(Roles = "Admin")]
    [Area("Admin")]

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
            List<Freelancer> freelancers = _unitOfWork.Freelancer.GetAll(includeProperties:"Category").ToList();
            
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
            if(id == 0 || id == null)
            {
                //create
                return View(freelancerVM);
            }

            else
            {
                //update
                freelancerVM.Freelancer = _unitOfWork.Freelancer.Get(u=>u.Id == id);
                return View(freelancerVM);

            }
        }
        [HttpPost]
        public IActionResult Upsert(FreelancerVM freelancerVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file !=  null)
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


                if (freelancerVM.Freelancer.Id == 0)
                {
                    _unitOfWork.Freelancer.Add(freelancerVM.Freelancer);
                }
                else
                {
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


            if (freelancerFromDb == null)
            {
                return NotFound();
            }
            return View(freelancerFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Freelancer? obj = _unitOfWork.Freelancer.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Freelancer.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Freelancer deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
