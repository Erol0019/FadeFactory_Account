using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Services;

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

        [HttpGet("{Id}")]
        public async Task<ActionResult<Account>> GetAccount(string AccountId)
        {
            Account account = await _service.GetAccount(AccountId);
            if (account.AccountId == "-1") return NotFound($"No account with ID '{AccountId}'");
            return Ok(account);
        }

        [HttpGet("getAll")]
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
            account.AccountId = Guid.NewGuid().ToString();

            Account createdAccount = await _service.CreateAccount(account);

            var url = Url.RouteUrl("GetAccountByIdAsync", new { createdAccount.AccountId }, Request.Scheme);
            System.Console.WriteLine(url);

            String host = HttpContext.Request.Host.Value;
            String uri = $"https://{host}/api/Accounts/{createdAccount.AccountId}";

            return Created(uri, createdAccount);
            //return CreatedAtAction(nameof(GetAccountByIdAsync), new { createdAccount.Id }, createdAccount);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAccount(string Id)
        {
            var existingAccount = await _service.GetAccount(Id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _service.DeleteAccount(Id);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAccount(string Id, [FromBody] Account account)
        {
            if (Id != account.AccountId)
            {
                return BadRequest("Account ID mismatch");
            }

            var existingAccount = await _service.GetAccount(Id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            account.AccountId = Id;

            var updatedAccount = await _service.UpdateAccount(account);
            return Ok(updatedAccount);
        }
    }
}