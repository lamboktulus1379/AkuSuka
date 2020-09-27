using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> AccountsByOwner(Guid ownerId);

        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(Guid accountId);
        void CreateAccount(Account account);
    }
}