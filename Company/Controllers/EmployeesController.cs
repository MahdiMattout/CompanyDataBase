using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Models;
namespace Company.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyContext _db;
        public EmployeesController(CompanyContext db)
        {
            _db = db;
        }
        public IActionResult Create(string CompanyName)
        {
            ViewData["CompanyName"] = CompanyName;
            Console.WriteLine(ViewData["CompanyName"]);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string CompanyName, [Bind("EmployeeId,FirstName,LastName,Email,OfficeExtension,Address,OfficeNumber,Position,Ssn,CompanyName")] Employee employee)
        {
            employee.CompanyName = CompanyName;
            if (ModelState.IsValid)
            {
                _db.Add(employee);
                _db.SaveChanges();

                return RedirectToAction("Details", "Companies", new { Name = CompanyName });
            }
            return View(employee);
        }
        public IActionResult Delete(int? EmployeeId, string CompanyName)
        {
            if (EmployeeId == null)
            {
                return NotFound();
            }

            var employee = _db.Employees
                .FirstOrDefault(m => m.EmployeeId == EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["CompanyName"] = CompanyName;
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int EmployeeId, string CompanyName)
        {
            var employee = _db.Employees.Find(EmployeeId, CompanyName);
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Details", "Companies", new { Name = CompanyName });
        }
        public IActionResult Edit(int? EmployeeId, string CompanyName)
        {
            if (EmployeeId == null)
            {
                return NotFound();
            }

            var employee = _db.Employees.Find(EmployeeId, CompanyName);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int EmployeeId, string CompanyName, [Bind("EmployeeId,FirstName,LastName,Email,OfficeExtension,Address,OfficeNumber,Position,Ssn,CompanyName")] Employee employee)
        {
            if (EmployeeId != employee.EmployeeId)
            {
                return NotFound();
            }
            employee.CompanyName = CompanyName;

            if (ModelState.IsValid)
            {
                _db.Update(employee);
                _db.SaveChanges();


                return RedirectToAction("Details", "Companies", new { Name = CompanyName });
            }
            return View(employee);
        }

        public IActionResult Type(int EmployeeId, string CompanyName)
        {
            var HourlyPaid = _db.HourlyPaids.Where(e => e.HourlyEmployeeId == EmployeeId).FirstOrDefault();
            if(HourlyPaid != null)
            {
                return RedirectToAction("Details", "HourlyPaidEmployee", new { EmployeeId, CompanyName });
            }
            else
            {
                return RedirectToAction("Details", "MonthlyPaidEmployee", new { EmployeeId, CompanyName });
            }
        }
    }
}
