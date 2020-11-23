using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public User CheckUser(string username, string password)
        {
            return FindByCondition(user => user.Username.Equals(username) && user.Password.Equals(password)).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return FindByCondition(user => user.Username.Equals(username)).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
