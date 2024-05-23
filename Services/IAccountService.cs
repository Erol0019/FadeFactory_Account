using FadeFactory_Accounts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FadeFactory_Accounts.Services;
public interface IAccountService
{
    Task<Account> GetAccount(int AccountId);
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> CreateAccount(AccountDTO account);
    Task<Account> UpdateAccount(Account account);
    Task DeleteAccount(int AccountId);
    Task<string> Login(AccountDTO loginRequest);
}

