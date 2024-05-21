using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FadeFactory_Accounts.Models;
using FadeFactory_Accounts.Data;
using System.Threading.Tasks;

namespace FadeFactory_Accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<Account>> GetAccountByIdAsync(string Id)

        {
            var tasks = await _accountRepository.GetAccountByIdAsync(Id);
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            account.Id = Guid.NewGuid().ToString();

            var createdAccount = await _accountRepository.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccountByIdAsync), new { createdAccount.Id }, createdAccount);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAccount(string Id)
        {
            var existingAccount = await _accountRepository.GetAccountByIdAsync(Id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _accountRepository.DeleteAccountAsync(Id);
            return NoContent();
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateAccount(string Id, [FromBody] Account account)
        {
            if (Id != account.Id)
            {
                return BadRequest("Account ID mismatch");
            }

            var existingAccount = await _accountRepository.GetAccountByIdAsync(Id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            account.Id = Id;

            var updatedAccount = await _accountRepository.UpdateAccountAsync(account);
            return Ok(updatedAccount);

        }
    }
}