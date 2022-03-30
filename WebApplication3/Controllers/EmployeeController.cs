using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext DbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<Employee> emps = DbContext.Employees.ToList();
            return View(emps);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                DbContext.Employees.Add(emp);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(emp);
            }
           
        }

        public IActionResult Delete(int id)
        {
            var emp = DbContext.Employees.SingleOrDefault(e=> e.Id==id);

            if (emp!=null)
            {
                DbContext.Employees.Remove(emp);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        public IActionResult Edit(int id)
        {
            var emp = DbContext.Employees.SingleOrDefault(e => e.Id == id);
            return View (emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            DbContext.Employees.Update(emp);
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
