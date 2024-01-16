
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository context)
        {
            _categoryRepo = context;
        }
        public  IActionResult Index()
        {
            List<Category> objCategoryList =  _categoryRepo.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display connot exactly match the Name.");

            }
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _categoryRepo.Add(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");

        }

        public  IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
       
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display connot exactly match the Name.");

            }
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            _categoryRepo.Update(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");

        }



        public  IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id); ;
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePostAsync(int? id)
        {
            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id); ;
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(categoryFromDb);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }

    }
}
