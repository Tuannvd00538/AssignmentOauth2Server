using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;
using SecurityHelper;

namespace AssignmentOauth2Server.Controllers
{
    [Route("_api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;

        public AccountsController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET: _api/v1/Accounts
        [HttpGet]
        public IEnumerable<Account> GetAccount()
        {
            return _context.Account;
        }

        // GET: _api/v1/Accounts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Account.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: _api/v1/Accounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount([FromRoute] long id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != account.Id)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: _api/v1/Accounts/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> PostAccount([FromBody] AccountInfomation accountInfomation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            accountInfomation.Account.Salt = PasswordHandle.GetInstance().GenerateSalt();
            accountInfomation.Account.Password = PasswordHandle.GetInstance().EncryptPassword(accountInfomation.Account.Password, accountInfomation.Account.Salt);
            if (AccountExistsByEmail(accountInfomation.Account.Email))
            {
                return Conflict("Tài khoản có email " + accountInfomation.Account.Email + " đã tồn tại trên hệ thống, vui lòng kiểm tra lại!");
            }
            else
            {
                _context.AccountInfomation.Add(accountInfomation);
                await _context.SaveChangesAsync();
            }

            return Ok("Tạo tài khoản thành công!");
        }

        // DELETE: _api/v1/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Account.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(account);
        }

        private bool AccountExists(long id)
        {
            return _context.Account.Any(e => e.Id == id);
        }

        private bool AccountExistsByEmail(string email)
        {
            return _context.Account.Any(e => e.Email == email);
        }
    }
}