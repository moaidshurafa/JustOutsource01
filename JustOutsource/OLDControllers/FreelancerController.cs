//using JustOutsource.DataAccess.Data;
//using JustOutsource.Models;
//using JustOutsource.Utility;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace JustOutsource.Controllers
//{

//    [Authorize(Roles = "Freelancer,Admin")]

//    public class FreelancerController : Controller
//    {
//        private readonly ApplicationDbContext _db;
//        public FreelancerController(ApplicationDbContext db)
//        {
//            _db = db;
//        }
//        public IActionResult Index()
//        {
//            var freelancers = _db.Freelancers.Include(f => f.Category).ToList();
//            return View(freelancers);
//        }
//        public IActionResult Details(int id)
//        {
//            var job = _db.Jobs.Include(f => f.Category).FirstOrDefault(f => f.Id == id);
//            if (job == null)
//            {
//                return NotFound();
//            }

//            var jobVM = new JobVM
//            {
//                Job = job,
//            };

//            return View(jobVM);
//        }
//        public IActionResult FindJob()
//        {
//            var jobs = _db.Jobs.Include(j => j.Category).ToList();
//            return View(jobs);
//        }
//        public IActionResult Create()
//        {
//            FreelancerVM freelancerVM = new()
//            {
//                CategoryList = _db.Categories.Select(u => new SelectListItem
//                {
//                    Text = u.CategoryName,
//                    Value = u.Id.ToString()
//                }),
//                Freelancer = new Freelancer()
//            };
//            return View(freelancerVM);
//        }
//        [HttpPost]
//        public IActionResult Create(FreelancerVM obj)
//        {
//            if(ModelState.IsValid)
//            {
//                _db.Freelancers.Add(obj.Freelancer);
//                _db.SaveChanges();
//                TempData["success"] = "Freelancer added successfully";
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                obj.CategoryList = _db.Categories.Select(u => new SelectListItem
//                {
//                    Text = u.CategoryName,
//                    Value = u.Id.ToString()
//                });
//                return View(obj);
//            }
//        }
//        public IActionResult Edit(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound();
//            }
//            var freelancerFromDb = _db.Freelancers.Find(id);
//            if (freelancerFromDb == null)
//            {
//                return NotFound();
//            }
//            var freelancerVM = new FreelancerVM
//            {
//                Freelancer = freelancerFromDb,
//                CategoryList = _db.Categories.Select(c => new SelectListItem
//                {
//                    Text = c.CategoryName,
//                    Value = c.Id.ToString()
//                })
//            };
//            return View(freelancerVM);
//        }
//        [HttpPost]
//        public IActionResult Edit(FreelancerVM obj)
//        {

//            if (ModelState.IsValid)
//            {
//                _db.Freelancers.Update(obj.Freelancer);
//                _db.SaveChanges();
//                TempData["success"] = "Freelancer updated successfully";
//                return RedirectToAction("Index");
//            }
//            obj.CategoryList = _db.Categories.Select(c => new SelectListItem
//            {
//                Text = c.CategoryName,
//                Value = c.Id.ToString()
//            });
//            return View(obj);
        
//        }
//        public IActionResult Delete(int? id)
//        {
//            if (id == null || id == 0)
//            {
//                return NotFound();
//            }
//            var freelancerFromDb = _db.Freelancers.Find(id);
//            if (freelancerFromDb == null)
//            {
//                return NotFound();
//            }
//            var freelancerVM = new FreelancerVM
//            {
//                Freelancer = freelancerFromDb,
//                CategoryList = _db.Categories.Select(c => new SelectListItem
//                {
//                    Text = c.CategoryName,
//                    Value = c.Id.ToString()
//                })
//            };
//            return View(freelancerVM);
//        }
//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeletePOST(int? id)
//        {
//            Freelancer? obj = _db.Freelancers.Find(id);

//            if (obj == null)
//            {
//                return NotFound();
//            }
//            _db.Freelancers.Remove(obj);
//            _db.SaveChanges();
//            TempData["success"] = "Freelancer deleted successfully";
//            return RedirectToAction("Index");

//        }

//    }
//}
