using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;
using Microsoft.AspNetCore.Http;
using SecurityHelper;
using MlkPwgen;

namespace AssignmentOauth2Server.Controllers
{
    public class AdminController : Controller
    {
        private readonly AssignmentOauth2ServerContext _context;

        public AdminController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Account.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDay,Phone")] AccountInfomation account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Email,Password,Salt,RollNumber,CreatedAt,UpdatedAt,DeletedAt,Status")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Email,Password")] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existAccount = _context.Account.SingleOrDefault(a => a.Email == login.Email);
            if (existAccount != null)
            {
                if (existAccount.Password == PasswordHandle.GetInstance().EncryptPassword(login.Password, existAccount.Salt))
                {
                    HttpContext.Session.SetString("loggedUserEmail", existAccount.Email);
                    HttpContext.Session.SetString("loggedUserId", existAccount.Id.ToString());
                    var existCredential = await _context.Credential.SingleOrDefaultAsync(c =>
                            c.OwnerId == existAccount.Id);
                    if (existCredential != null)
                    {
                        var accessToken = PasswordGenerator.Generate(length: 40, allowed: Sets.Alphanumerics);
                        existCredential.AccessToken = accessToken;
                        HttpContext.Session.SetString("loggedUserToken", accessToken);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var credential = new Credential(existAccount.Id)
                        {
                            AccessToken = PasswordGenerator.Generate(length: 40, allowed: Sets.Alphanumerics)
                        };
                        HttpContext.Session.SetString("loggedUserToken", credential.AccessToken);
                        _context.Credential.Add(credential);
                        await _context.SaveChangesAsync();
                    }
                    return Redirect("/");
                }
                return BadRequest("Mật khẩu không chính xác!");
            }
            return BadRequest("Email hoặc mật khẩu không chính xác!");
        }

        private bool AccountExists(long id)
        {
            return _context.Account.Any(e => e.Id == id);
        }
    }
}
