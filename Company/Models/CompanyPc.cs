using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class CompanyPc
    {
        public CompanyPc()
        {
            PcUsers = new HashSet<PcUser>();
        }

        public string NameOfUser { get; set; }
        public string CompanyPcId { get; set; }
        public string NameofCompany { get; set; }
        public float TotalMemory { get; set; }
        public float AverageMemoryUsage { get; set; }
        public string MemoryModel { get; set; }
        public float ClockSpeed { get; set; }
        public float AverageCpuUsage { get; set; }
        public string CpuModel { get; set; }
        public string Cpumanufacturer { get; set; }
        public float DiskSpace { get; set; }
        public string DiskModel { get; set; }
        public float ReadWriteSpeed { get; set; }
        public int? Idadmin { get; set; }

        public virtual Admin IdadminNavigation { get; set; }
        public virtual Company NameofCompanyNavigation { get; set; }
        public virtual ICollection<PcUser> PcUsers { get; set; }
    }
}
