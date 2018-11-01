using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BackSiteTemplate.Interface.IdentityServices;

namespace BackSiteTemplate.Controllers
{
    /// <summary>
    /// 如該頁面沒有加入Page Control或為預設顯示頁面則不用加入此屬性
    /// </summary>
    public class IPWhiteList : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BackSiteDBTempContext db = new BackSiteDBTempContext();
            var UserPublicIP = new GetUserPublicIP().ip;
            //取得Public IP後先進行White List過濾
            var Result = db.WhiteList.Where(o => o.Ip == UserPublicIP).AsNoTracking().Any();
            if (!Result)
            {
                //當IP不在White List內 回傳StatusCode = 400
                filterContext.RouteData.Values["controller"] = "Home";
                filterContext.RouteData.Values["action"] = "Error";
                filterContext.HttpContext.Response.StatusCode = 400;
            }
            base.OnActionExecuting(filterContext);
            
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
    }
}
