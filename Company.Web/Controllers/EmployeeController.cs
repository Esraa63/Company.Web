using Company.Data.Entitis;
using Company.Service.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService , IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        [HttpGet]
        public IActionResult Index( string searchIndex)
        {
            //ViewBag , ViewData , TempData
            ViewBag.Message = "Hello From Employee Index (View Bag)";
            ViewData["TextMessage"]= "Hello From Employee Index (View Data)";
            
            IEnumerable<Employee> employees = new List<Employee>();
            if (string.IsNullOrEmpty(searchIndex))
                employees = _employeeService.GetAll();
            else
             employees = _employeeService.GetEmployeeByName(searchIndex);
              
            return View(employees);
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            var departments = _departmentService.GetAll();
            return View();
        }
        [HttpPost]
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
