using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;

namespace AssignmentOauth2Server.Controllers
{
    [Route("_api/v1/[controller]")]
    [ApiController]
    public class HandleClientAPIController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;

        public HandleClientAPIController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET: _api/v1/HandleClientAPI
        [HttpGet]
        public IEnumerable<Account> GetAccount()
        {
            return _context.Account;
        }

        // GET: _api/v1/HandleClientAPI/Class/32
        // OwnerID
        [Route("Class/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass([FromRoute] long id)
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
            else
            {
                var getClassByIdStudent = await _context.Classes.Where(a => a.OwnerId == account.Id).ToListAsync();
                List<Class> classList = new List<Class>();
                foreach (var item in getClassByIdStudent)
                {
                    classList.Add(_context.Class.SingleOrDefault(a => a.Id == item.ClassId));
                }
                return new JsonResult(classList);
            }
        }

        // GET: _api/v1/HandleClientAPI/Logs/1
        // OwnerID
        [Route("Logs/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject([FromRoute] long id)
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
            else
            {
                var getLogsByStudentId = await _context.Log.Where(a => a.OwnerId == account.Id).ToListAsync();
                return new JsonResult(getLogsByStudentId);
                //List<AccountLogsDefault> logsDefault = new List<AccountLogsDefault>();
                //List<AccountLogsMark> logsMark = new List<AccountLogsMark>();
                //List<AccountLogsMail> logsMail = new List<AccountLogsMail>();
                //foreach (var item in getLogsByStudentId)
                //{
                //    if (item.Default != null)
                //    {
                //        logsDefault.Add(_context.Default.SingleOrDefault(a => a.Id == item.Default.Id));
                //    }
                //    if (item.Mark != null)
                //    {
                //        logsMark.Add(_context.Mark.SingleOrDefault(a => a.Id == item.Mark.Id));
                //    }
                //    if (item.Mail != null)
                //    {
                //        logsMail.Add(_context.Mail.SingleOrDefault(a => a.Id == item.Mail.Id));
                //    }
                //}
                //if (logsDefault != null)
                //{
                //    return new JsonResult(logsDefault);
                //}
                //else if (logsMark != null)
                //{
                //    return new JsonResult(logsMark);
                //}
                //else if (logsMail != null)
                //{
                //    return new JsonResult(logsMail);
                //}
            }
            return new JsonResult("SayHi()");
        }

        // PUT: _api/v1/HandleClient_api/v1/5
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

        // POST: _api/v1/HandleClientAPI
        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Account.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: _api/v1/HandleClient_api/v1/5
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
    }
}