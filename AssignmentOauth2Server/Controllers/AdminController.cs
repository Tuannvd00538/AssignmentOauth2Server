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
        public async Task<IActionResult> Create()
        {
            ViewData["Roles"] = _context.Role.ToList();
            ViewData["Class"] = _context.Class.ToList();
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,BirthDay,Phone")] AccountInfomation accountInfomation, int[] classIds, int roleId)
        {
            //return new JsonResult(roleId);
            //if (ModelState.IsValid)
            //{
            //    List<Class> listClass = new List<Class>();
            //    foreach (var classId in classIds) {
            //       var clazz = _context.Class.SingleOrDefault(s=>s.Id == classId);
            //        if (clazz == null) {
            //            return new JsonResult("Lỗi");
            //        }
            //        listClass.Add(clazz);
            //    }

            //    _context.Add(account);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(account);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //string[] listTypeRole = { "A", "D", "M" };

            //if (!listTypeRole.Contains(Rnb))
            //{
            //    return BadRequest();
            //}
            var Rnb = "";
            switch (roleId)
            {
                case 1:
                    Rnb = "A";
                    break;
                case 2:
                    Rnb = "M";
                    break;
                case 3:
                    Rnb = "D";
                    break;
                default:
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

                foreach(var item in classIds)
                {
                    Classes classes = new Classes
                    {
                        OwnerId = account.Id,
                        ClassId = item.ToString()
                    };
                    _context.Classes.Add(classes);
                }

                await _context.SaveChangesAsync();
            }


            return Created("", login);
        }

        private bool AccountExistsByPhone(string phone)
        {
            return _context.AccountInfomation.Any(e => e.Phone == phone);
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
                string[] listTypeRole = { "A", "M" };
                var email = "";
                if (existAccount.RollNumber.Any())
                {
                    email = existAccount.RollNumber[0].ToString();
                }
                if (!listTypeRole.Contains(email.ToUpper()))
                {
                    HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return new JsonResult("Bạn không có quyền truy cập!");
                }
                else if (existAccount.Password == PasswordHandle.GetInstance().EncryptPassword(login.Password, existAccount.Salt))
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
