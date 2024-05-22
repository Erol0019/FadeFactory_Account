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

        [HttpGet("{AccountId}")]
        public async Task<ActionResult<Account>> GetAccount(int AccountId)
        {
            Account account = await _service.GetAccount(AccountId);
            if (account.AccountId == -1) return NotFound($"No account with ID '{AccountId}'");
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
            try
            {
                int dbSize = (await _service.GetAllAccounts()).LastOrDefault()?.AccountId ?? 0;
                account.AccountId = dbSize + 1;

                Account createdAccount = await _service.CreateAccount(account);

                String host = HttpContext.Request.Host.Value;
                String uri = $"https://{host}/api/Accounts/{createdAccount.AccountId}";

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
    }
}