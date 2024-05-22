using FadeFactory_Accounts.Models;

namespace FadeFactory_Accounts.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccount(string AccountId);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccount(Account account);
        Task DeleteAccount(string AccountId);
    }
}