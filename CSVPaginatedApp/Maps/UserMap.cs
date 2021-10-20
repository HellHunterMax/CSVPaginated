using CSVPaginatedApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Maps
{
    public static class UserMap
    {
        private static readonly NumberStyles Style = NumberStyles.AllowCurrencySymbol | NumberStyles.Currency;
        private static readonly CultureInfo Provider = new("en-US");

        /// <summary>
        /// Maps A User from CSV file line.
        /// </summary>
        /// <param name="line">should be one line from the CSV file.</param>
        /// <returns></returns>
        public static User Map(string line)
        {
            //id,first_name,last_name,birth_date,salary
            string[] splitUser = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            splitUser[3] = CorrectTheDateTimeFormat(splitUser[3]);
            TrimSalary(splitUser);

            User user = new()
            {
                Id = int.Parse(splitUser[0]),
                FirstName = splitUser[1],
                LastName = splitUser[2],
                BirthDate = DateTime.ParseExact(splitUser[3], "MM/dd/yyyy", CultureInfo.InvariantCulture), // string.IsNullOrWhiteSpace(splitUser[3]) ? new DateTime() : 
                Salary = Decimal.Parse(splitUser[4], Style, Provider)
            };
            return user;
        }

        private static void TrimSalary(string[] splitUser)
        {
            splitUser[4] = splitUser[4].Trim(new char[] { '/', '"' });
        }

        private static string CorrectTheDateTimeFormat(string v)
        {
            string date = "01/01/0001";
            if (v.Length != 0)
            {
                string[] splitDateTime = v.Split('/');
                if (splitDateTime[0].Length == 1)
                {
                    splitDateTime[0] = "0" + splitDateTime[0];
                }
                if (splitDateTime[1].Length == 1)
                {
                    splitDateTime[1] = "0" + splitDateTime[1];
                }
                date = $"{splitDateTime[0]}/{splitDateTime[1]}/{splitDateTime[2]}";
            }
            return date;
        }
    }
}
