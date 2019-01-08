using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;
using SecurityHelper;
using Newtonsoft.Json;

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

            var accInfo = _context.AccountInfomation.SingleOrDefault(a => a.OwnerId == account.Id);

            return Ok(JsonConvert.SerializeObject(new { account.Email, account.RollNumber, accInfo.FirstName, accInfo.LastName, accInfo.Avatar, accInfo.BirthDay, accInfo.Gender, accInfo.Phone }));
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

        // POST: _api/v1/Accounts/A~D~M
        [HttpPost("{Rnb}")]
        public async Task<IActionResult> PostAccount([FromRoute] string Rnb, [FromBody] AccountInfomation accountInfomation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string[] listTypeRole = { "A", "D", "M" };

            if (!listTypeRole.Contains(Rnb))
            {
                return BadRequest();
            }

            //Generate RollNumber
            var count = await _context.Account.CountAsync(a => a.RollNumber.Contains(Rnb)) + 1;
            string rollNumber;

            if (count < 10)
            {
                rollNumber = "0000" + count;
            }
            else if (count < 100)
            {
                rollNumber = "000" + count;
            }
            else if (count < 1000)
            {
                rollNumber = "00" + count;
            }
            else if (count < 10000)
            {
                rollNumber = "0" + count;
            }
            else
            {
                rollNumber = count.ToString();
            }

            var rnber = (Rnb + rollNumber).ToLower();

            // Generate Email
            var str = accountInfomation.FirstName.Split(" ");
            string email = accountInfomation.LastName;
            foreach (var item in str)
            {
                if (item.Any())
                {
                    email += item[0];
                }
            }

            email = email.ToLower();

            var emailGenerate = RemoveUTF8.RemoveSign4VietnameseString(email + rnber + "@siingroup.com").ToLower();
            var passwordGenerate = RemoveUTF8.RemoveSign4VietnameseString(email + rnber);

            //Create new account
            Account account = new Account
            {
                RollNumber = rnber,
                Email = emailGenerate,
                Salt = PasswordHandle.GetInstance().GenerateSalt()
            };
            account.Password = PasswordHandle.GetInstance().EncryptPassword(passwordGenerate, account.Salt);

            _context.Account.Add(account);
            
            //Create thông tin đăng nhập để trả về response
            Login login = new Login
            {
                Email = emailGenerate,
                Password = passwordGenerate
            };

            //Check uniqe by phone
            if (AccountExistsByPhone(accountInfomation.Phone))
            {
                return Conflict("Tài khoản đã tồn tại trên hệ thống, vui lòng kiểm tra lại!");
            }
            else
            {
                //Save account
                await _context.SaveChangesAsync();

                //Get ra account.Id để gán cho FK ownerId bên accountinfomation
                accountInfomation.OwnerId = account.Id;
                _context.AccountInfomation.Add(accountInfomation);
                await _context.SaveChangesAsync();
            }


            return Created("", login);
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

        private bool AccountExistsByPhone(string phone)
        {
            return _context.AccountInfomation.Any(e => e.Phone == phone);
        }
    }
}