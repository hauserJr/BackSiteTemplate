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
    public static class RequestCultureMiddlewareExtensions
    {
        /// <summary>
        /// 統一使用Error Page
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        //public static IApplicationBuilder UseCustomError(
        //        this IApplicationBuilder builder)
        //{
        //    return builder.UseMiddleware<CustomError>();
        //}

    }
}
