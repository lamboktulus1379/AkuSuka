namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        IProductRepository Product { get; }
        IUserRepository User { get; }
        void Save();
    }
}