using BullyBook.Data;
using BullyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BullyBook.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList(); // to list na lekhe ni huncha afai list ma haldincha 
            return View(objCategoryList);
        }

        //create get from view model
        public IActionResult Create()
        {

            return View();
        }
        //create post to database
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order cannot be same ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "category created successfully";
                return RedirectToAction("Index");//if u want to define controller u have to give comma and write controller name "Index","Category"

            }
            return View(obj);
        }
        //Edit get from view model
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Edit post to database
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display order cannot be same ");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "category Updated successfully";
                return RedirectToAction("Index");//if u want to define controller u have to give comma and write controller name "Index","Category"

            }
            return View(obj);
        }
        //Delete get from view model
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Delete post to database
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {


            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "category Deleted successfully";
            return RedirectToAction("Index");//if u want to define controller u have to give comma and write controller name "Index","Category"
        }
    }
}
