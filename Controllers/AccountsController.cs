using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;
using Microsoft.AspNetCore.Authorization;

namespace FadeFactory_Accounts.Controllers
{
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

        [HttpGet("getAll"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _service.GetAllAccounts();
            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            int dbSize = (await _service.GetAllAccounts()).LastOrDefault()?.AccountId ?? 0;
            account.AccountId = dbSize + 1;

            Account createdAccount = await _service.CreateAccount(account);

            var url = Url.RouteUrl("GetAccountByIdAsync", new { createdAccount.AccountId }, Request.Scheme);
            System.Console.WriteLine(url);

            String host = HttpContext.Request.Host.Value;
            String uri = $"https://{host}/api/Accounts/{createdAccount.AccountId}";

            return Created(uri, createdAccount);
            //return CreatedAtAction(nameof(GetAccountByIdAsync), new { createdAccount.Id }, createdAccount);
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
    }
}