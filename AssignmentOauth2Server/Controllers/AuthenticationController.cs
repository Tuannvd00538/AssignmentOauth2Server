using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Models;
using Microsoft.Extensions.Caching.Memory;
using SecurityHelper;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using System.Net;
using MlkPwgen;

namespace AssignmentOauth2Server.Controllers
{
    [Route("_api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;

        public AuthenticationController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // POST: _api/v1/Authentication/Login
        [HttpPost]
        [Route("Login")]
        [EnableCors(PolicyName = "AllowAll")]
        public async Task<IActionResult> PostLogin([FromBody] Login login)
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
                    var existCredential = await _context.Credential.SingleOrDefaultAsync(c =>
                            c.OwnerId == existAccount.Id);
                    if (existCredential != null)
                    {
                        var accessToken = PasswordGenerator.Generate(length: 40, allowed: Sets.Alphanumerics);
                        existCredential.AccessToken = accessToken;
                        await _context.SaveChangesAsync();
                        return Ok(existCredential);
                    }
                    else
                    {
                        var credential = new Credential(existAccount.Id)
                        {
                            AccessToken = PasswordGenerator.Generate(length: 40, allowed: Sets.Alphanumerics)
                        };
                        _context.Credential.Add(credential);
                        await _context.SaveChangesAsync();
                        return Ok(credential);
                    }
                }
                return BadRequest("Mật khẩu không chính xác!");
            }
            return BadRequest("Email hoặc mật khẩu không chính xác!");
        }
        
        public IActionResult CheckToken(string accessToken)
        {
            var credential = _context.Credential.SingleOrDefault(t => t.AccessToken == accessToken);
            if (credential == null || !credential.IsValid())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return new JsonResult("Invalid token");
            }
            else
            {
                return new JsonResult(credential);
            }
        }

        // POST: _api/v1/Authentication/ChangePassword
        [HttpPost]
        [Route("ChangePassword")]
        [EnableCors(PolicyName = "AllowAll")]
        public async Task<IActionResult> PostChangePassword([FromBody] ChangePassword changepsw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existAccount = _context.Account.SingleOrDefault(a => a.Id == changepsw.OwnerId);
            if (existAccount != null)
            {
                if (existAccount.Password == PasswordHandle.GetInstance().EncryptPassword(changepsw.OldPassword, existAccount.Salt))
                {
                    existAccount.Password = PasswordHandle.GetInstance().EncryptPassword(changepsw.NewPassword, existAccount.Salt);
                    await _context.SaveChangesAsync();
                    return new JsonResult("Đổi mật khẩu thành công!");
                }
                return BadRequest("Mật khẩu cũ không chính xác!");
            }
            else
            {
                return new JsonResult("Tài khoản không tồn tại!");
            }
        }
    }
}