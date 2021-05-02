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
        public IActionResult Details(int EmployeeId, string CompanyName)
        {
            return View();
        }
    }
}
