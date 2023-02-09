
using BookStore2022.DataAccess.Repository.IRepository;
using BookStore2022.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Index
        public IActionResult Index()
        {
            
            IEnumerable<CoverType> coverTypeList = _unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
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
        public IActionResult Create(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType created sucessfully";
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
            var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (CoverTypeFromDbFirst == null)
            { return NotFound(); }


            return View(CoverTypeFromDbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
          
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType updated sucessfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            { return NotFound(); }

            var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (CoverTypeFromDb == null)
            { return NotFound(); }


            return View(CoverTypeFromDb);
        }
        //POST
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var CovertypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            if (CovertypeFromDb == null)
            { return NotFound(); }

            _unitOfWork.CoverType.Remove(CovertypeFromDb);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted sucessfully";
            return RedirectToAction("Index");
        }


    }
}

