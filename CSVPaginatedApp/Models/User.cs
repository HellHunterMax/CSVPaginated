using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Models
{
    public class User
    {
        //id,first_name,last_name,birth_date,salary
        [Name("id")]
        public int Id { get; set; }
        [Name("first_name")]
        public string FirstName { get; set; }
        [Name("last_name")]
        public string LastName { get; set; }
        [Name("birth_date")]
        public DateTime BirthDate { get; set; }
        [Name("salary")]
        public Decimal Salary { get; set; }


        public int GetAge()
        {
            if (BirthDate.Date == new DateTime(01,01,0001).Date)
            {
                return -1;
            }
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            if (BirthDate.Month < today.Month && BirthDate.Day < today.Day)
            {
                age--;
            }

            return age;
        }

        public override string ToString()
        {
            var date = BirthDate == DateTime.ParseExact("01/01/0001", "MM/dd/yyyy", CultureInfo.InvariantCulture) ? "" : BirthDate.ToShortDateString();

            return $"{Id}, {FirstName}, {LastName}, {date}, {string.Format(new CultureInfo("en-US", false), "{0:c}", Salary)}";
        }
    }
}
