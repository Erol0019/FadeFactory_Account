using FadeFactory_Accounts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FadeFactory_Accounts.Services
{
    public interface IAccountService
    {
        Task<Account> GetAccount(int AccountId);
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccount(Account account);
        Task DeleteAccount(int AccountId);
        Task<Account> RegisterAccount(Account account);
        Task<string> Login(Account loginRequest);
    }
}
