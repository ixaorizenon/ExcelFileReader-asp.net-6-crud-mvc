using FoodApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly TestDBContext _dbContext;

        public EmployeesController(TestDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Member addEmployeeRequest)
        {
                var employee = new Uyeler()
                {
                    MemberName = addEmployeeRequest.MemberName,
                    MemberSurname = addEmployeeRequest.MemberSurname,
                    MemberMail = addEmployeeRequest.MemberMail,
                    MemberPassword = addEmployeeRequest.MemberPassword,
                };
            await _dbContext.Uyelers.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Add");
            
        }

    }

}
