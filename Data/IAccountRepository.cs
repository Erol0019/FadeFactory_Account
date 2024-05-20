using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using FadeFactory_Accounts.Models;
using Microsoft.AspNetCore.Mvc;

namespace FadeFactory_Accounts.Data
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountByIdAsync(string Id);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<ActionResult<IEnumerable<Account>>> GetAccountByFirstNameAsync(string firstName);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(string Id);
    }
}