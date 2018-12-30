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

namespace AssignmentOauth2Server.Controllers
{
    [Route("_api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AssignmentOauth2ServerContext _context;
        private IMemoryCache _cache;

        public AuthenticationController(AssignmentOauth2ServerContext context)
        {
            _context = context;
        }

        // POST: _api/v1/Authentication
        [HttpPost]
        [EnableCors(PolicyName = "AllowAll")]
        public async Task<IActionResult> PostLogin([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginSuccess = false;
            var existAccount = _context.Account.SingleOrDefault(a => a.Email == login.Email);
            if (existAccount != null)
            {
                if (existAccount.Password == PasswordHandle.GetInstance().EncryptPassword(login.Password, existAccount.Salt))
                {
                    loginSuccess = true;
                }
            }

            if (loginSuccess)
            {
                var credential = new Credential(existAccount.Id);
                _context.Credential.Add(credential);
                await _context.SaveChangesAsync();
                Response.StatusCode = 200;
                return new JsonResult(credential);
            };
            Response.StatusCode = 403;
            return new JsonResult("Invalid information");
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
    }
}