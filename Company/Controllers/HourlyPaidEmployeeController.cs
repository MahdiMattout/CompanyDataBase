using Company.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class HourlyPaidEmployeeController : Controller
    {
        private readonly CompanyContext _db;

        public HourlyPaidEmployeeController(CompanyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(string CompanyName)
        {
            ViewData["CompanyName"] = CompanyName;
            return View();
        }
        public IActionResult Details(int EmployeeId)
        {
            var hourlyPaidEmployee = _db.HourlyPaids.Find(EmployeeId);
            if (hourlyPaidEmployee == null) return NotFound();
            var employee = _db.Employees.Find(EmployeeId);
            employee.Relatives = _db.Relatives.Where(e => e.EmployeeId == EmployeeId).ToList();
            ViewData["Employee"] = employee;
            return View(hourlyPaidEmployee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(int? EmployeeId, string Relationship)
        {
            ViewData["Relationship"] = Relationship;
            var HourlyPaidEmployee = _db.HourlyPaids.Find(EmployeeId);
            if (HourlyPaidEmployee == null) return NotFound();
            var employee = _db.Employees.Find(EmployeeId);
            employee.Relatives = _db.Relatives.Select(p => p).Where(x => x.EmployeeId.Equals(employee.EmployeeId) && x.Relationship.Equals(Relationship)).ToList();
            ViewData["Employee"] = employee;
            return View(HourlyPaidEmployee);
        }
    }
}
