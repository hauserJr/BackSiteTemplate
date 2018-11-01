using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
    public class PageFilter : ActionFilterAttribute
    {
        private readonly ServiceProvider ClaimsProvider;
        public PageFilter()
        {
            this.ClaimsProvider = new ServiceCollection().AddScoped<IIdentityAction, IdentityService>().BuildServiceProvider();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BackSiteDBTempContext db = new BackSiteDBTempContext();
            var UserPublicIP = new GetUserPublicIP().ip;
            //取得使用者Claim
            var UserData = this.ClaimsProvider.GetService<IIdentityAction>().GetClaim(filterContext.HttpContext.User.Identity);
            //使用者本地IP 192.168.x.x etc.
            var UserIP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();

            //取得Controller nad Action
            var ControllerName = filterContext.RouteData.Values["controller"].ToString();
            var ActionName = filterContext.RouteData.Values["action"].ToString();

            //頁面Id Init
            int SubNavsId = -1;
            //取得此頁頁面的ID
            var GetSubNavsId = db.SubNavs.Where(o => o.ControllerName == ControllerName && o.ActionName == ActionName).FirstOrDefault();
            SubNavsId = GetSubNavsId == null ? SubNavsId : GetSubNavsId.Id;

            bool Pass = false;

            //當身分為Super Admin則不需要做其他判斷
            if (UserData.SuperAdmin)
            {
                Pass = true;
            }
            else
            {

                var PageQuery = db.SubNavs
                    .Where(o => o.ControllerName == ControllerName && o.ActionName == ActionName)
                    .Select(o => o.RoleScope.Where(r => r.RoleGroupId == UserData.RoleGroupId && r.SubNavsId == o.Id).FirstOrDefault())
                    .FirstOrDefault();
                if (PageQuery != null)
                {
                    //驗證通過
                    Pass = true;
                    SubNavsId = PageQuery.SubNavsId;
                }
                else
                {
                    //不通過
                    filterContext.RouteData.Values["controller"] = "Error";
                    filterContext.RouteData.Values["action"] = "Index";
                }
            }

            var Para = JsonConvert.SerializeObject(filterContext.ActionArguments.ToList(), Formatting.Indented);
            db.PageFilterLogs.Add(new PageFilterLogs()
            {
                UserId = UserData.Id.Value,
                RoleGroupId = UserData.RoleGroupId,
                SubNavsId = SubNavsId,
                PublicIp = UserPublicIP,
                ClientIp = UserIP,
                Pass = Pass,
                Para = Para
            });
            db.SaveChanges();
            
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
