using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoryController(ApplicationDBContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _dbContext.Categories;
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Cannot save both with same name!");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)  
            {
                return NotFound();
            }
            var categoryFromDB = _dbContext.Categories.Find(id);
            // var categoryFromDBFirst = _dbContext.Categories.FirstOrDefault(u=>u.Id==id);
            // var categoryFromDBSingle = _dbContext.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Cannot save both with same name!");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = _dbContext.Categories.Find(id);
            // var categoryFromDBFirst = _dbContext.Categories.FirstOrDefault(u=>u.Id==id);
            // var categoryFromDBSingle = _dbContext.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _dbContext.Categories.Find(id);
            // var categoryFromDBFirst = _dbContext.Categories.FirstOrDefault(u=>u.Id==id);
            // var categoryFromDBSingle = _dbContext.Categories.SingleOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }


            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
