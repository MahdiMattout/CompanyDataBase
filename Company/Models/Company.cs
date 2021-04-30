using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyPcs = new HashSet<CompanyPc>();
            Employees = new HashSet<Employee>();
        }

        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }
        public int? Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public virtual ICollection<CompanyPc> CompanyPcs { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
