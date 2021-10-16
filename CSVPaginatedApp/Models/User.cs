using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Models
{
    public class User
    {
        //id,first_name,last_name,birth_date,salary

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Decimal Salary { get; set; }
    }
}
