using CSVPaginatedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVPaginatedApp.Repository
{
    interface IUserRepository
    {
        Task<List<User>> GetUsers(int amount, int page);
    }
}
