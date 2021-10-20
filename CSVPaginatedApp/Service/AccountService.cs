using CSVPaginatedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Service
{
    public class AccountService : IAccountService
    {
        public Task<IEnumerable<User>> GetUsersAsync(int amount, int page)
        {
            //@"C:\Code_Projects\2021\CSVPaginated\CSVPaginated\CSVPaginatedApp\Data\targets.csv"
            throw new NotImplementedException();
        }
    }
}
