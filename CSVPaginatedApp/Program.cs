using CSVPaginatedApp.Repository;
using System;
using System.Threading.Tasks;

namespace CSVPaginatedApp
{
    class Program
    {
        [STAThread]
        static async Task Main(string[] args)
        {
            //TODO EXTRA: Filters. Age and Salary
            //TODO EXTRA OrderBy lastname and firstname Salary Age

            UserRepository repo = new();

            var users = await repo.GetUsers(10, 4);

            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }
    }
}
