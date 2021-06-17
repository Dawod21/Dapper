using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperCrud.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperCrud.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(Employee.GetEmployees());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind]Employee obj)
        {
            if (ModelState.IsValid)
            {
                if (Employee.AddEmployee(obj)>0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee emp = Employee.GetEmployeeById(id);
            if (emp == null)
                return NotFound();
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind]Employee obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                Employee.UpdateEmployee(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            Employee emp = Employee.GetEmployeeById(id);
            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }
        [HttpPost]
        public IActionResult Delete(int id, Employee obj)
        {
            if (Employee.DeleteEmployee(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}