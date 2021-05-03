using Company.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class MonthlyPaidEmployeeController : Controller
    {
        private readonly CompanyContext _db;

        public MonthlyPaidEmployeeController(CompanyContext db)
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
            var MonthlyPaidEmployee = _db.MonthlyPaids.Find(EmployeeId);
            if (MonthlyPaidEmployee == null) return NotFound();
            var employee = _db.Employees.Find(EmployeeId);
            employee.Relatives = _db.Relatives.Where(e => e.EmployeeId == EmployeeId).ToList();
            ViewData["Employee"] = employee;
            return View(MonthlyPaidEmployee);
        }
    }
}
