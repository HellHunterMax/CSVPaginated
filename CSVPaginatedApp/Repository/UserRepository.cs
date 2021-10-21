using CSVPaginatedApp.Maps;
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
    public class UserRepository : IUserRepository
    {
        private readonly string _Path;

        public UserRepository(string path)
        {
            ValidatePath(path);
            _Path = path;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var lines = await File.ReadAllLinesAsync(_Path);

            List<User> users = new();
            foreach (var line in lines)
            {
                try
                {
                    User user = UserMap.Map(line);
                    users.Add(user);
                }
                catch (Exception)
                {
                    Console.WriteLine("Could not map this:");
                    Console.WriteLine(line);
                }
            }

            return users;
        }
        public int GetNumberOfPages(int amount)
        {
            return File.ReadLines(_Path).Count() / amount;
        }

        private static void ValidatePath(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("No valid Path was given.");
            }
            if (!File.Exists(path))
            {
                throw new ArgumentNullException("No valid Path was given."); //TODO create Exception for path.
            }
            //TODO create check to see if its CSV and an account file.
        }
    }
}
