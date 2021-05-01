using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly CompanyContext _db;

        public CompaniesController(CompanyContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var companies = _db.Companies.ToList();
            ViewData["Companies"] = companies;
            return View();
        }

        public IActionResult Edit(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var company = _db.Companies.Where(c => c.Name.Equals(name)).FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string name, [Bind("Name,Country,City,PhoneNumber,ContactEmail")] Models.Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(company);
                    _db.SaveChanges();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return NotFound();
                }
                return RedirectToAction("Index", "Companies");
            }
            return View(company);
        }
    }
}
