using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class AdminsOfDangerousPc
    {
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
        public long CountDPcId { get; set; }
    }
}
