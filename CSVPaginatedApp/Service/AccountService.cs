using CSVPaginatedApp.Models;
using CSVPaginatedApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _Repo;

        public AccountService(IUserRepository repo)
        {
            _Repo = repo;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int amount, int page)
        {
            //@"C:\Code_Projects\2021\CSVPaginated\CSVPaginated\CSVPaginatedApp\Data\targets.csv"

            int lineStart = page * amount;

            var users = await _Repo.GetUsersAsync();
            List<User> usersToReturn = CreateUsersListPaginated(amount, lineStart, users);

            return usersToReturn;
        }

        private static List<User> CreateUsersListPaginated(int amount, int lineStart, IEnumerable<User> users)
        {
            var usersToReturn = new List<User>();

            int i = 0;
            foreach (var user in users)
            {
                if (usersToReturn.Count >= amount)
                {
                    break;
                }
                if (i < lineStart)
                {
                    i++;
                    continue;
                }
                usersToReturn.Add(user);
                i++;
            }

            return usersToReturn;
        }
    }
}
