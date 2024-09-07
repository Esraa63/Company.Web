using Company.Data.Entitis;
using Company.Service.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public IActionResult Index( string searchIndex)
        {
            if(string.IsNullOrEmpty(searchIndex))
            {
                var employees = _employeeService.GetAll();
                return View(employees);
            }
            else
            {
                var employees = _employeeService.GetEmployeeByName(searchIndex);
                return View(employees);
            }
        }
        [HttpGet]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    return RedirectToAction("Index");
                }
                else
                {
                  //  ModelState.AddModelError("EmployeeError", "ValidationError");
                    return View(employee);
                }
            }
            catch (Exception ex) 
            {
               // ModelState.AddModelError("EmployeeError", ex.Message);
                return View(employee);
            }
           
        }
        //[HttpPost]
        //public IActionResult Update()
        //{

        //}
        //[HttpPost]
        //public IActionResult Delete() 
        //{
        //}
    }
}
