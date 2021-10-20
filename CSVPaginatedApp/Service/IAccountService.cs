using CSVPaginatedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Service
{
    public interface IAccountService
    {
        Task<IEnumerable<User>> GetUsersAsync(int amount, int page);
    }
}
