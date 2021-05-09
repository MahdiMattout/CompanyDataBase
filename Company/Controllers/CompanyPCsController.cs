using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
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
            foreach(var pc in companypcs)
            {
                pc.AverageCpuUsage = new Random().Next(0, 100);
                pc.AverageMemoryUsage = new Random().Next(0, 100);
            }
            _db.SaveChanges();
            ViewData["CompanyPCs"] = companypcs;
            ViewData["CompanyName"] = CompanyName;
            return View();
        }
        public IActionResult Create(string CompanyName)
        {
            ViewData["AdminId"] = "";
            ViewData["EmployeeId"] = "";
            ViewData["CompanyName"] = CompanyName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string CompanyName,[Bind("PcId,AdminId,EmployeeId,CpuModel,Cpumanufacturer,ClockSpeed,DiskModel,DiskSpace,ReadWriteSpeed,MemoryModel,TotalMemory")] CompanyPc companyPc)
        {
            var admin = _db.Employees.Where(e => e.EmployeeId == companyPc.AdminId && e.IsAdmin == 1 && e.CompanyName.Equals(CompanyName)).FirstOrDefault();
            var employee = _db.Employees.Where(e => e.EmployeeId == companyPc.EmployeeId && e.CompanyName.Equals(CompanyName)).FirstOrDefault();

            if (employee != null)
            {
                companyPc.NameOfUser = $"{employee.FirstName} {employee.LastName}";
            }
            else
            {
                companyPc.EmployeeId = -1;
                ViewData["EmployeeId"] = "-1";
                ViewData["CompanyName"] = CompanyName;
                return View(companyPc);
            }
            if(admin == null)
            {
                ViewData["AdminId"] = "-1";
                ViewData["CompanyName"] = CompanyName;
                return View(companyPc);
            }
            companyPc.NameofCompany = CompanyName;
            companyPc.AverageCpuUsage = new Random().Next(0,100);
            companyPc.AverageMemoryUsage = new Random().Next(0, 100);
            if (ModelState.IsValid)
            {
                _db.Add(companyPc);
                _db.SaveChanges();
                return RedirectToAction("DisplayPCs", "CompanyPCs", new {CompanyName });
            }
            ViewData["CompanyName"] = CompanyName;
            return View(companyPc);
        }
        public IActionResult Delete(int? PcId)
        {
            /*if (PcId == null)
            {
                return NotFound();
            }*/

            var companypc = _db.CompanyPcs
                .FirstOrDefault(m => m.PcId == PcId);
            /*if (companypc == null)
            {
                return NotFound();
            }*/

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
            

            var companypc = _db.CompanyPcs.Find(PcId);
            
            return View(companypc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int PcId,string CompanyName,[Bind("PcId,AdminId,EmployeeId,CpuModel,Cpumanufacturer,ClockSpeed,DiskModel,DiskSpace,ReadWriteSpeed,MemoryModel,TotalMemory")] CompanyPc companyPc)
        {
            
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
        public IActionResult DisplayDangerousPcs(string CompanyName)
        {
            var pcs = _db.DangerousPcsViews.Where(pc=> pc.NameofCompany.Equals(CompanyName)).ToList();
            ViewData["CompanyName"] = CompanyName;
            return View(pcs);
        }
        public IActionResult DisplayHealthyPcs(string CompanyName)
        {
            var pcs = _db.HealthyPcs.Where(pc=> pc.NameofCompany.Equals(CompanyName)).ToList();
            ViewData["CompanyName"] = CompanyName;
            return View(pcs);
        }
        public IActionResult DisplayAdminsOfDangerousPCs(string CompanyName)
        {
            var admins = _db.AdminsOfDangerousPcs.Where(admin => admin.CompanyName.Equals(CompanyName)).ToList();

            ViewData["CompanyName"] = CompanyName;
            return View(admins);
        }
       
    }
}