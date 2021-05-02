using Company.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class RelativesController : Controller
    {
        private readonly CompanyContext _db;
        public RelativesController(CompanyContext db)
        {
            _db = db;
        }
        public IActionResult Create(int EmployeeId)
        {
            ViewData["EmployeeId"] = EmployeeId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int EmployeeId, [Bind("Name,Relationship")] Relative relative)
        {
            relative.EmployeeId = EmployeeId;
            if (ModelState.IsValid)
            {
                _db.Add(relative);
                _db.SaveChanges();
                return RedirectToAction("Details", "Employees", new { EmployeeId });
            }
            return View(relative);
        }
    }
}
