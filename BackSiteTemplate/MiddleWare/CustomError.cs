using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.MiddleWare
{
    public class CustomError
    {
        private readonly RequestDelegate _next;
        public CustomError(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Error Page(暫不啟用)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Response.StatusCode >= 400)
            {
                context.Request.Path = "/Home/Error";
                context.Response.StatusCode = 200;
            }
            await _next(context);
        }
    }
}
