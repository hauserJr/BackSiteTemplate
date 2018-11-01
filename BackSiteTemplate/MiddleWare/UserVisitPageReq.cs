using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.MiddleWare
{
    public class UserVisitPageReq
    {
        private readonly RequestDelegate _next;
        public UserVisitPageReq(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 使用者訪問頁面的Filter (有問題暫不啟用)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            BackSiteDBTempContext db = new BackSiteDBTempContext();
            var UserPublicIP = new GetUserPublicIP().ip;
            var Result = db.WhiteList.Where(o => o.Ip == UserPublicIP).AsNoTracking().Any();
            if (!Result)
            {
                context.Response.StatusCode = 400;
            }
            await _next(context);
        }
    }
}
