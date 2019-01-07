using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public IActionResult CheckLogin(string abc)
        {
            //var loggedIdString = HttpContext.Session.GetString("loggedUserToken");
            return new JsonResult("SayHi()");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool isValid = context.Request.Headers.ContainsKey("Authorization");

            if (isValid)
            {

                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            else
            {
                await context.Response.WriteAsync("Access denied.");
            }


        }
    }
}
