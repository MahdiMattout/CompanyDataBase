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

        public IActionResult Delete(int? EmployeeId, string RelativeName)
        {
            if (EmployeeId == null)
            {
                return NotFound();
            }

            var relative = _db.Relatives.Where(e => e.EmployeeId == EmployeeId && RelativeName.Equals(e.Name)).FirstOrDefault();
            if (relative == null)
            {
                return NotFound();
            }

            return View(relative);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int EmployeeId, string RelativeName)
        {
            var relative = _db.Relatives.Where(e => e.EmployeeId == EmployeeId && RelativeName.Equals(e.Name)).FirstOrDefault();
            _db.Relatives.Remove(relative);
            _db.SaveChanges();
            return RedirectToAction("Details", "Employees", new { EmployeeId });
        }
    }
}
