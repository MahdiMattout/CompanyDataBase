using System;
using System.Collections.Generic;

#nullable disable

namespace Company.Models
{
    public partial class PcUser
    {
        public int PcuserId { get; set; }
        public string Username { get; set; }
        public string PcId { get; set; }

        public virtual CompanyPc Pc { get; set; }
        public virtual Employee Pcuser { get; set; }
    }
}
