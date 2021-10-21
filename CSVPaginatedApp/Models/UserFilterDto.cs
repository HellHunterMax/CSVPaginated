using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Models
{
    public class UserFilterDto
    {
        public int MinimumAge { get; set; } = int.MinValue;
        public int MaximumAge { get; set; } = int.MaxValue;
        public decimal MinimumSalary { get; set; } = decimal.MinValue;
        public decimal MaximumSalary { get; set; } = decimal.MaxValue;
    }
}
