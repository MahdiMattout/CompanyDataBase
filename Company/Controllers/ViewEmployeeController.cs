using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Models;
namespace Company.Controllers
{
    public class ViewEmployeeController : Controller
    {
        private readonly CompanyContext _db;
        public ViewEmployeeController(CompanyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var employees = _db.Employees.ToList();
            ViewData["Employees"] = employees;
            return View();
        }
    }
}
