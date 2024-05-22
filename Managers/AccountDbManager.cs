using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;
using Microsoft.EntityFrameworkCore;

namespace FadeFactory_Accounts.Managers
{
    public class AccountDbManager : IAccountService
    {
        private readonly AccountDbContext _context;

        public AccountDbManager(AccountDbContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            var result = _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAccount(int AccountId)
        {
            _context.Accounts.Remove(new Account { AccountId = AccountId });
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccount(int AccountId)
        {
            if (_context.Accounts == null) throw new NullReferenceException();
            var result = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == AccountId);
            if (result == null) throw new Exception("Account not found.");
            return result;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            var result = await _context.Accounts.ToListAsync();
            return result;
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            var result = _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}