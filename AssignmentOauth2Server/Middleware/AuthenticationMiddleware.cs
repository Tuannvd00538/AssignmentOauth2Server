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

        public async Task InvokeAsync(HttpContext context)
        {
            //bool isValid = context.Request.Headers.ContainsKey("Authorization");
            var accessToken = context.Session.GetString("loggedUserToken");

            if (accessToken != null || context.Request.Path.ToString().Contains("/_api/v1/") || context.Request.Path.ToString().Contains("/Admin/Login"))
            {
                await _next(context);
            }
            else {
                context.Response.Redirect("/Admin/Login");
            }


        }
    }
}
