using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User CheckUser(string username, string password);

        User GetUserByUsername(string username);
        void UpdateUser(User user);
    }
}
