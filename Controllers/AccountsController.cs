using Microsoft.AspNetCore.Mvc;
using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;
using System;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FadeFactory_Accounts.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountsController(IAccountService service)
    {
        _service = service;
    }

    [HttpGet("{AccountId}")]
    public async Task<ActionResult<Account>> GetAccount(int AccountId)
    {
        Account account = await _service.GetAccount(AccountId);
        if (account.AccountId == -1) return NotFound($"No account with ID '{AccountId}'");
        return Ok(account);
    }

    [HttpGet("getAll"), Authorize]
    public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
    {
        try
        {
            var accounts = await _service.GetAllAccounts();
            return Ok(accounts);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<Account>> CreateAccount(Account account)
    {
        try
        {
            var createdAccount = await _service.RegisterAccount(account);
            string host = HttpContext.Request.Host.Value;
            string uri = $"https://{host}/api/Accounts/{createdAccount.AccountId}";
            return Created(uri, createdAccount);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Account with email already exists."))
            {
                return Conflict(new { message = "Email is already in use" });
            }

            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{AccountId}")]
    public async Task<IActionResult> DeleteAccount(int AccountId)
    {
        try
        {
            await _service.DeleteAccount(AccountId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateAccount([FromBody] Account account)
    {
        try
        {
            Account updatedAccount = await _service.UpdateAccount(account);
            return Ok(updatedAccount);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(Account request)
    {
        try
        {
            string token = await _service.Login(request);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
