using Company.Data.Entitis;
using Company.Repository.Interfaces;
using Company.Service.InterFaces;
using Company.Service.InterFaces.Department.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

         public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
           // TempData.Keep("TempTextMessage");
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(department);
                    TempData["TempTextMessage"] = "Hello From Employee Index (Temp Data)";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("DepartmentError", "Validation Errors");
                return View(department);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("DepartmentError", ex.Message);
                return View(department);
            }
         
        }
        public IActionResult Details(int? id, string viewName = "Details") 
        {
            var department =_departmentService.GetById(id);

            if (department is null)
                return RedirectToAction("NotFoundPage",null,"Home");

                return View(viewName,department);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int? id, Department department)
        {
            if(department.Id != id.Value)
                return RedirectToAction("NotFoundPage", null, "Home");
                _departmentService.Update(department);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id) 
        {
            var department = _departmentService.GetById(id);

            if (department is null)
                return RedirectToAction("NotFoundPage", null, "Home");
            _departmentService.Delete(department);
            return RedirectToAction(nameof(Index));
        }
    }
}
