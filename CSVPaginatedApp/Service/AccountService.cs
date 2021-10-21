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
        private readonly IUserRepository _repo;
        private int _pages;

        public AccountService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int amount, int page, UserFilterDto filter)
        {
            int lineStart = page * amount;

            var users = await _repo.GetUsersAsync();

            users = users.Where(user => (user.GetAge() == -1 ||
                                        (user.GetAge() >= filter.MinimumAge &&
                                        user.GetAge() <= filter.MaximumAge)) &&
                                        user.Salary >= filter.MinimumSalary &&
                                        user.Salary <= filter.MaximumSalary);

            UpdateNumberOfPages(amount, users);

            if (lineStart >= _pages * amount)
            {
                lineStart = (_pages -1) * amount;
            }

            List<User> usersToReturn = CreateUsersListPaginated(amount, lineStart, users);

            return usersToReturn;
        }

        private void UpdateNumberOfPages(int amount, IEnumerable<User> users)
        {
            _pages = users.Count() / amount;
            if (users.Count() % amount != 0)
            {
                _pages++;
            }
        }

        public int GetNumberOfPages(int amount)
        {
            return _pages;
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
