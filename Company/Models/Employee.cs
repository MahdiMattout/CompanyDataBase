using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class Employee
    {
        public Employee()
        {
            CompanyPcs = new HashSet<CompanyPc>();
            Relatives = new HashSet<Relative>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? OfficeExtension { get; set; }
        public string Address { get; set; }
        public int? OfficeNumber { get; set; }
        public string Position { get; set; }
        public int Ssn { get; set; }
        public string CompanyName { get; set; }
        public byte IsAdmin { get; set; }

        public virtual Company CompanyNameNavigation { get; set; }
        public virtual HourlyPaid HourlyPaid { get; set; }
        public virtual MonthlyPaid MonthlyPaid { get; set; }
        public virtual ICollection<CompanyPc> CompanyPcs { get; set; }
        public virtual ICollection<Relative> Relatives { get; set; }
    }
}
