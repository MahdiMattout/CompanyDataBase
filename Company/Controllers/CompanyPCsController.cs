using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.Models;

namespace Company.Controllers
{
    public class CompanyPCsController : Controller
    {
        private readonly CompanyContext _db;

        public CompanyPCsController(CompanyContext db)
        {
            _db = db;
        }

        public IActionResult DisplayPcs(string CompanyName)
        {
            var companypcs = _db.CompanyPcs.Where(p=>p.NameofCompany.Equals(CompanyName)).ToList();
            ViewData["CompanyPCs"] = companypcs;
            ViewData["CompanyName"] = CompanyName;
            return View();
        }
        public IActionResult Create(string CompanyName)
        {
            ViewData["CompanyName"] = CompanyName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string CompanyName,[Bind("PcId,AdminId,EmployeeId,CpuModel,Cpumanufacturer,ClockSpeed,DiskModel,DiskSpace,ReadWriteSpeed,MemoryModel,TotalMemory")] CompanyPc companyPc)
        {
            var admin = _db.Employees.Where(e => e.EmployeeId == companyPc.AdminId && e.IsAdmin == 1).FirstOrDefault();
            var employee = _db.Employees.Where(e => e.EmployeeId == companyPc.EmployeeId).FirstOrDefault();
            if (admin == null)
            {
                return NotFound();
            }
            if (employee!= null)
            {
                companyPc.NameOfUser = $"{employee.FirstName} {employee.LastName}";
            }
            companyPc.NameofCompany = CompanyName;
            companyPc.AverageCpuUsage =new Random().Next(0,100);
            companyPc.AverageMemoryUsage = new Random().Next(0, 100); 
            if (ModelState.IsValid)
            {
                _db.Add(companyPc);
                _db.SaveChanges();
                return RedirectToAction("DisplayPCs", "CompanyPCs", new {CompanyName });
            }
            return View(companyPc);
        }
        public IActionResult Delete(int? PcId)
        {
            if (PcId == null)
            {
                return NotFound();
            }

            var companypc = _db.CompanyPcs
                .FirstOrDefault(m => m.PcId == PcId);
            if (companypc == null)
            {
                return NotFound();
            }

            return View(companypc);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int PcId)
        {
            var companypc = _db.CompanyPcs.Where(e => e.PcId == PcId).FirstOrDefault();
            _db.CompanyPcs.Remove(companypc);
            _db.SaveChanges();
            return RedirectToAction("DisplayPCs", "CompanyPCs", new { CompanyName = companypc.NameofCompany});
        }
        public IActionResult Edit(int? PcId)
        {
            if (PcId == null)
            {
                return NotFound();
            }

            var companypc = _db.CompanyPcs.Find(PcId);
            if (companypc == null)
            {
                return NotFound();
            }
            return View(companypc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int PcId,string CompanyName,[Bind("PcId,AdminId,EmployeeId,CpuModel,Cpumanufacturer,ClockSpeed,DiskModel,DiskSpace,ReadWriteSpeed,MemoryModel,TotalMemory")] CompanyPc companyPc)
        {
            if (PcId != companyPc.PcId)
            {
                return NotFound();
            }
            companyPc.NameofCompany = CompanyName;
            var employee = _db.Employees.Where(e => e.EmployeeId == companyPc.EmployeeId).FirstOrDefault();
            companyPc.AverageCpuUsage = new Random().Next(0, 100);
            companyPc.AverageMemoryUsage = new Random().Next(0, 100);
            if (employee != null)
            {
                companyPc.NameOfUser = $"{employee.FirstName} {employee.LastName}";
            }
            if (ModelState.IsValid)
            {
                _db.Update(companyPc);
                _db.SaveChanges();


                return RedirectToAction("DisplayPCs", "CompanyPCs", new { CompanyName });
            }
            return View(companyPc);
        }

    }
}