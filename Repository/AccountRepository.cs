using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        private ISortHelper<Account> _accountHelper;
        public AccountRepository(RepositoryContext repositoryContext, ISortHelper<Account> accountHelper)
            : base(repositoryContext)
        {
            _accountHelper = accountHelper;
        }

        public PagedList<Account> GetAccontsByOwner(Guid ownerId, AccountParameters parameters)
        {
            var accounts = FindByCondition(a => a.OwnerId.Equals(ownerId));
            var sortedAccounts = _accountHelper.ApplySort(accounts, parameters.OrderBy);

            return PagedList<Account>.ToPagedList(sortedAccounts, parameters.PageNumber, parameters.PageSize);
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();
        }

        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }



        public Account GetAccountById(Guid accountId)
        {
            return FindByCondition(a => a.Id.Equals(accountId)).FirstOrDefault();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                .OrderBy(ow => ow.DateCreated)
                .ToList();
        }

        public void UpdateAccount(Account account)
        {
            Update(account);
        }
    }
}