

using BookStore2022.DataAccess.Repository.IRepository;
using BookStore2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Index
        public IActionResult Index()
        {
            //instead of declaring var we could use IEnumerable <category> then we don't need to mention _db.Categories.ToList();
            //instead _db.Categories;
            IEnumerable<Category> categorylist = _unitOfWork.Category.GetAll();
            return View(categorylist);
        }

        //Create functionalities
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Displayorder cannot match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //Delete functionalities
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            { return NotFound(); }
            //var categoryFromDb = _db.Categories.Find(id);
            var CategoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);
            //var CategoryFromDbSingle = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (CategoryFromDbFirst == null)
            { return NotFound(); }


            return View(CategoryFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Displayorder cannot match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            { return NotFound(); }

            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            { return NotFound(); }


            return View(categoryFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            { return NotFound(); }

            _unitOfWork.Category.Remove(categoryFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted sucessfully";
            return RedirectToAction("Index");
        }


    }
}

