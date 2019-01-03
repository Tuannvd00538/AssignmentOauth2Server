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
    public class AccountInfomationsController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;

        public AccountInfomationsController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET:_api/v1/AccountInfomations
        [HttpGet]
        public IEnumerable<AccountInfomation> GetAccountInfomation()
        {
            return _context.AccountInfomation;
        }

        // GET:_api/v1/AccountInfomations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountInfomation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountInfomation = await _context.AccountInfomation.FindAsync(id);

            if (accountInfomation == null)
            {
                return NotFound();
            }

            return Ok(accountInfomation);
        }

        // PUT:_api/v1/AccountInfomations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountInfomation([FromRoute] int id, [FromBody] AccountInfomation accountInfomation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountInfomation.Id)
            {
                return BadRequest();
            }

            _context.Entry(accountInfomation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountInfomationExists(id))
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

        // POST: _api/v1/AccountInfomations
        [HttpPost]
        public async Task<IActionResult> PostAccountInfomation([FromBody] AccountInfomation accountInfomation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            accountInfomation.Account.Salt = PasswordHandle.GetInstance().GenerateSalt();
            accountInfomation.Account.Password = PasswordHandle.GetInstance().EncryptPassword(accountInfomation.Account.Password, accountInfomation.Account.Salt);

            _context.AccountInfomation.Add(accountInfomation);
            await _context.SaveChangesAsync();

            return new JsonResult("Success!");
        }

        // DELETE:_api/v1/AccountInfomations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountInfomation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountInfomation = await _context.AccountInfomation.FindAsync(id);
            if (accountInfomation == null)
            {
                return NotFound();
            }

            _context.AccountInfomation.Remove(accountInfomation);
            await _context.SaveChangesAsync();

            return Ok(accountInfomation);
        }

        private bool AccountInfomationExists(int id)
        {
            return _context.AccountInfomation.Any(e => e.Id == id);
        }
    }
}