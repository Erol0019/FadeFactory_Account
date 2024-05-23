using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FadeFactory_Accounts.Helpers;
using System.Net;

namespace FadeFactory_Accounts.Managers;

public class AccountDbManager : IAccountService
{
    private readonly AuthHelper _authHelper;
    private readonly AccountDbContext _context;

    public AccountDbManager(AccountDbContext context)
    {
        _authHelper = new AuthHelper();
        _context = context;
    }

    public async Task<Account> CreateAccount(AccountDTO accountDTO)
    {
        int dbSize = (await _context.Accounts.ToListAsync()).LastOrDefault()?.AccountId ?? 0;
        accountDTO.AccountId = dbSize + 1;

        if (_context.Accounts.Count(a => a.Email == accountDTO.Email) > 0)
            throw new Exception("Account with email already exists.");

        var account = accountDTO.Adapt();
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
        if (_context.Accounts.Count(a => a.Email == account.Email && a.AccountId != account.AccountId) > 0)
        {
            throw new Exception("Account with this email already exists.");
        }

        var result = _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<string> Login(AccountDTO loginRequest)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == loginRequest.Email) ?? throw new Exception("Invalid credentials.");
        if (!_authHelper.VerifyPasswordHash(loginRequest.Password, account.PasswordHash, account.PasswordSalt))
        {
            throw new Exception("Invalid credentials.");
        }
        return _authHelper.CreateToken(account);
    }
}
