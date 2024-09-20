using Company.Data.Entites;
using Company.Service.InterFaces;
using Company.Service.InterFaces.Employee.Dto;
using Company.Service.Services;
using Company.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index(string searchIndex)
        {
            //ViewBag , ViewData , TempData
            //ViewBag.Message = "Hello From Employee Index (View Bag)";
            //ViewData["TextMessage"]= "Hello From Employee Index (View Data)";
            //ViewBag , ViewData , TempData
            //ViewBag.Message = "Hello From Employee Index (View Bag)";
            //ViewData["TextMessage"]= "Hello From Employee Index (View Data)";

            IEnumerable<EmployeeDto> employees = new List<EmployeeDto>();
            if (string.IsNullOrEmpty(searchIndex))
                employees = _employeeService.GetAll();
            else
                employees = _employeeService.GetEmployeeByName(searchIndex);

            return View(employees);

        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto employeeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employeeDto);
                    //return RedirectToAction("Index");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("EmployeeError", "ValidationError");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("EmployeeError", ex.Message);
                return View(employeeDto);
            }

        }
        public IActionResult Details(int? id, string viewName = "Details")
        {
            var employee = _employeeService.GetById(id);

            if (employee is null)
                return RedirectToAction("NotFoundPage", null, "Home");

            return View(viewName, employee);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int? id, EmployeeDto employeeDto)
        {
            if (employeeDto.Id != id.Value)
                return RedirectToAction("NotFoundPage", null, "Home");
            _employeeService.Update(employeeDto);
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);

            if (employee is null)
                return RedirectToAction("NotFoundPage", null, "Home");
            _employeeService.Delete(employee);
            return RedirectToAction(nameof(Index));
        }
    }

}