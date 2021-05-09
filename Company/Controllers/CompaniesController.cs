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
            // query 1
            var companies = _db.Companies.ToList();
            ViewData["Companies"] = companies;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index(string CompanySearch)
        { 
            ViewData["GetCompanies"] = CompanySearch;
            // query 1
            var companyquery = from x in _db.Companies select x;
            if (!String.IsNullOrEmpty(CompanySearch))
            {
                // query 2
                companyquery = companyquery.Where(x => x.Name.Contains(CompanySearch));
            }
            ViewData["Companies"] = await companyquery.AsNoTracking().ToListAsync();
            return View();
        }
        public IActionResult Edit(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }
            // query 3
            var company = _db.Companies.Where(c => c.Name.Equals(name)).FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string name, [Bind("Name,Country,City,Zip,PhoneNumber,ContactEmail")] Models.Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // query 4
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Country,City,Zip,PhoneNumber,ContactEmail")] Models.Company company)
        {
            if (ModelState.IsValid)
            {
                // query 5
                _db.Add(company);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }
        public  IActionResult Delete(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }
            // query 3
            var company = _db.Companies
                .FirstOrDefault(m => m.Name == name);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(string name)
        {
            // query 3
            var company = _db.Companies.Find(name);

            // query 6
            _db.Companies.Remove(company);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(string? name)
        {
            if (name == null)
            {
                return NotFound();
            }
            // query 3
            var company = _db.Companies
                .FirstOrDefault(m => m.Name == name);
            if (company == null)
            {
                return NotFound();
            }
            // query 7
            company.Employees = _db.Employees.Select(p => p).Where(x => x.CompanyName == company.Name).ToList();
            return View(company);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(string? Name, string EmployeeFirstName)
        {
            ViewData["GetEmployees"] = EmployeeFirstName;
            if (Name == null)
            {
                return NotFound();
            }
            // query 8
            var company = _db.Companies
                .FirstOrDefault(m => m.Name == Name);
            if (company == null)
            {
                return NotFound();
            }
            // query 8
            company.Employees = _db.Employees.Select(p => p).Where(x => x.CompanyName.Equals(company.Name) && x.FirstName.Equals(EmployeeFirstName)).ToList();
            return View(company);
        }
        public IActionResult DisplayLowSalaryEmployees(string CompanyName)
        {
            // query 7
            var employyesInSpecificCompany = _db.Employees.Where(employee => employee.CompanyName.Equals(CompanyName)).ToList();
            // query 9
            var employees = _db.CountRelativesOfLowSalaryEmployees.ToList();
            var LowSalaryEmployeesInCompany = new List<CountRelativesOfLowSalaryEmployee>();
            foreach (var employee in employees)
            {
                foreach (var employeeInCompany in employyesInSpecificCompany)
                {
                    if (employeeInCompany.EmployeeId == employee.EmployeeId)
                    {
                        LowSalaryEmployeesInCompany.Add(employee);
                    }
                }
            }
            ViewData["CompanyName"] = CompanyName;
            return View(LowSalaryEmployeesInCompany);
        }
    }
}
