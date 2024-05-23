using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FadeFactory_Accounts.Helpers;

namespace FadeFactory_Accounts.Managers;

public class AccountDbManager : IAccountService
{
    private readonly AuthHelper _authHelper;
    private readonly AccountDbContext _context;

    public AccountDbManager(AccountDbContext context)
    {
        _context = context;
    }

    public async Task<Account> CreateAccount(Account account)
    {
        int dbSize = (await _context.Accounts.ToListAsync()).LastOrDefault()?.AccountId ?? 0;
        account.AccountId = dbSize + 1;

        if (_context.Accounts.Any(a => a.Email == account.Email))
            throw new Exception("Account with email already exists.");

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
        var result = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == AccountId);
        if (result == null) throw new Exception("Account not found.");
        return result;
    }

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account> UpdateAccount(Account account)
    {
        if (_context.Accounts.Any(a => a.Email == account.Email && a.AccountId != account.AccountId))
        {
            throw new Exception("Account with this email already exists.");
        }

        var result = _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Account> RegisterAccount(Account account)
    {
        _authHelper.CreatePasswordHash(Encoding.UTF8.GetString(account.PasswordHash), out byte[] passwordHash, out byte[] passwordSalt);
        account.PasswordHash = passwordHash;
        account.PasswordSalt = passwordSalt;

        return await CreateAccount(account);
    }

    public async Task<string> Login(Account loginRequest)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == loginRequest.Email);
        if (account == null || !_authHelper.VerifyPasswordHash(Encoding.UTF8.GetString(loginRequest.PasswordHash), account.PasswordHash, account.PasswordSalt))
        {
            throw new Exception("Invalid credentials.");
        }

        return _authHelper.CreateToken(account);
    }
}
