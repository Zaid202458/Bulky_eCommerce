
using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> objCategoryList = await _context.Categories.ToListAsync();
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
            _context.Categories.Add(obj);
            _context.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = await _context.Categories.FindAsync(id);
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
            _context.Categories.Update(obj);
            _context.SaveChanges();
            TempData["success"] = "Category Updated Successfully";
            return RedirectToAction("Index");

        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = await _context.Categories.FindAsync(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeletePostAsync(int? id)
        {
            Category? categoryFromDb = await _context.Categories.FindAsync(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(categoryFromDb);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }

    }
}
