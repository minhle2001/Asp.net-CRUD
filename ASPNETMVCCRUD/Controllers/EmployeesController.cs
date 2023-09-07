using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoDBContext mvcDemoDBContext;

        public EmployeesController(MVCDemoDBContext mvcDemoDBContext)
        {
            this.mvcDemoDBContext = mvcDemoDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoDBContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModal addEmployeeReq)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeReq.Name,
                Email = addEmployeeReq.Email,
                Salary = addEmployeeReq.Salary,
                DateOfBirth = addEmployeeReq.DateOfBirth,
                Department = addEmployeeReq.Department,
                Sex = addEmployeeReq.Sex,
            };
            await mvcDemoDBContext.Employees.AddAsync(employee);
            await mvcDemoDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mvcDemoDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModal()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department,
                    Sex = employee.Sex,
                };

                return await Task.Run(() => View("View",viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModal model)
        {
            var employee = await mvcDemoDBContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;
                employee.Sex = model.Sex;

                await mvcDemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModal model)
        {
            var employee = mvcDemoDBContext.Employees.Find(model.Id);

            if(employee != null)
            {
                mvcDemoDBContext.Employees.Remove(employee);
                await mvcDemoDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
