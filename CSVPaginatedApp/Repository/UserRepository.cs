using CSVPaginatedApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Repository
{
    //TODO rework the repo into the Service
    //HOW? Make the repo work for the service. Let the service ask for new data from the csv file.
    // Add model building to the model. or create a service for it. as its specific for this csv file.
    public class UserRepository : IUserRepository
    {
        private readonly NumberStyles Style = NumberStyles.AllowCurrencySymbol | NumberStyles.Currency;
        private readonly CultureInfo Provider = new("en-US");

        public async Task<IEnumerable<User>> GetUsersAsync(int amount, int page)
        {
            int lineStart = page * amount + 1;

            List<User> users = new();
            using (var reader = new StreamReader(@"C:\Code_Projects\2021\CSVPaginated\CSVPaginated\CSVPaginatedApp\Data\targets.csv"))
            {
                for (int i = 0; i < lineStart; i++)
                {
                    reader.ReadLine();
                }

                while (!reader.EndOfStream && amount != users.Count)
                {
                    string line = await reader.ReadLineAsync();
                    User user = CreateUserFromCsvLine(line);
                    users.Add(user);
                }
            }

            return users;
        }
        public int GetNumberOfPages(int amount)
        {
            return File.ReadLines(@"C:\Code_Projects\2021\CSVPaginated\CSVPaginated\CSVPaginatedApp\Data\targets.csv").Count() / amount;
        }

        private User CreateUserFromCsvLine(string line)
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
                if (splitDateTime[0].Length == 1 )
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
