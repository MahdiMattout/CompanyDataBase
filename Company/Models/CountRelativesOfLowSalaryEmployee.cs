using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class CountRelativesOfLowSalaryEmployee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int EmployeeId { get; set; }
        public long CountRelativesName { get; set; }
    }
}
