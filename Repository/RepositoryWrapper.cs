using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private IProductRepository _product;
        private ISortHelper<Owner> _ownerSortHelper;
        private ISortHelper<Account> _accountSortHelper;
        private readonly ISortHelper<Product> _productSortHelper;
        private IDataShaper<Owner> _ownerDataShaper;
        private IDataShaper<Account> _accountDataShaper;
        private readonly IDataShaper<Product> _productDataShaper;

        public IOwnerRepository Owner
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new OwnerRepository(_repoContext, _ownerSortHelper, _ownerDataShaper);
                }
                return _owner;
            }
        }
        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext, _accountSortHelper, _accountDataShaper);
                }
                return _account;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repoContext, _productSortHelper, _productDataShaper); 
                }
                return _product;
            }
        }
        public RepositoryWrapper(RepositoryContext repositoryContext, 
            ISortHelper<Owner> ownerSortHelper,
            ISortHelper<Account> accountSortHelper,
            ISortHelper<Product> productSortHelper,
            IDataShaper<Owner> ownerDataShaper,
            IDataShaper<Account> accountDataShaper,
            IDataShaper<Product> productDataShaper
            )
        {
            _repoContext = repositoryContext;
            _ownerSortHelper = ownerSortHelper;
            _accountSortHelper = accountSortHelper;
            _productSortHelper = productSortHelper;
            _ownerDataShaper = ownerDataShaper;
            _accountDataShaper = accountDataShaper;
            _productDataShaper = productDataShaper;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}