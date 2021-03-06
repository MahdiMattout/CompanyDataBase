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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string CompanyName, [Bind("EmployeeId,FirstName,LastName,Email,OfficeExtension,Address,OfficeNumber,Position,Ssn")] Employee employee, [Bind("HourlyWage,OvertimeWage")] HourlyPaid hourlyPaid, [Bind("Salary,Bonus")] MonthlyPaid monthlyPaid)
        {
            if(employee.Position.Equals("Admin"))
            {
                employee.IsAdmin = 1;
            }
            else
            {
                employee.IsAdmin = 0;
            }
         
            employee.CompanyName = CompanyName;
            if (monthlyPaid.Bonus != null)
                employee.MonthlyPaid = monthlyPaid;
            else
                employee.HourlyPaid = hourlyPaid;
            if (ModelState.IsValid)
            {
                // query 21
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
            // query 14
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
            // query 14
            var employee = _db.Employees.Where(e => e.EmployeeId == EmployeeId).FirstOrDefault();

            // query 22
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Details", "Companies", new { Name = CompanyName });
        }
        public IActionResult Edit(int? EmployeeId)
        {
            if (EmployeeId == null)
            {
                return NotFound();
            }
            // query 14
            var employee = _db.Employees.Find(EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int EmployeeId, string CompanyName, [Bind("EmployeeId,FirstName,LastName,Email,OfficeExtension,Address,OfficeNumber,Position,Ssn")] Employee employee)
        {
            if (EmployeeId != employee.EmployeeId)
            {
                return NotFound();
            }
            employee.CompanyName = CompanyName;

            if (ModelState.IsValid)
            {
                // query 23
                _db.Update(employee);
                _db.SaveChanges();


                return RedirectToAction("Details", "Companies", new { Name = CompanyName });
            }
            return View(employee);
        }

        public IActionResult Details(int EmployeeId)
        {
            // query 14
            var employee = _db.Employees.Find(EmployeeId);
            if(employee != null)
            {
                // query 24
                var hourlyPaid = _db.HourlyPaids.Find(EmployeeId);
                if(hourlyPaid != null)
                {
                    return RedirectToAction("Details", "HourlyPaidEmployee", new { EmployeeId });
                }
                else
                {
                    return RedirectToAction("Details", "MonthlyPaidEmployee", new { EmployeeId });
                }
            }
            return NotFound();
        }

        public IActionResult DisplayEmployee(string Companyname)
        {
            // query 25
            var dpc = _db.DangerousPcsViews.ToList();
            ViewData["DangerousPcs"] = dpc;

            return View();
        }
    }
}
