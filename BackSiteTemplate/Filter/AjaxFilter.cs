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
    public class AjaxFilter : ActionFilterAttribute
    {
        private readonly ServiceProvider ClaimsProvider;
        public AjaxFilter()
        {
            this.ClaimsProvider = new ServiceCollection().AddScoped<IIdentityAction, IdentityService>().BuildServiceProvider();
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            BackSiteDBTempContext db = new BackSiteDBTempContext();
            var UserPublicIP = new GetUserPublicIP().ip;

            //取得使用者Claims
            var UserData = this.ClaimsProvider.GetService<IIdentityAction>().GetClaim(filterContext.HttpContext.User.Identity);
            //取得User Local IP 192.168.x.x etc.
            var UserIP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();

            //取得Controller and Action
            var ControllerName = filterContext.RouteData.Values["controller"].ToString();
            var ActionName = filterContext.RouteData.Values["action"].ToString();

            //取得使用者傳入參數
            var Para = JsonConvert.SerializeObject(filterContext.ActionArguments.ToList(), Formatting.Indented);

            //取得使用的AJAX的SubNavsID
            var AjaxFuncData = db.AjaxFunctionList.Where(o => o.AjaxController == ControllerName && o.AjaxAction == ActionName).Select(o => new
            {
                SubNavsId = o.SubNavsId,
                AjaxId = o.Id
            }).FirstOrDefault();

            //確認使用者權限群組 是否有操作權限
            bool RoleScope = false;
            if (!UserData.SuperAdmin)
            {
                //當使用者非Super Admin 則需要依序確認確認
                //1.該Ajax Method綁定的頁面是哪一個
                //2.被綁定的頁面使用者有無操作權限
                RoleScope = db.RoleScope.Where(o => o.SubNavsId == AjaxFuncData.SubNavsId && o.RoleGroupId == UserData.RoleGroupId).Any();
            }

            //
            if (RoleScope)
            {
                //當使用者非Super Admin且擁有權限時
                //記錄使用者操作紀錄
                db.AjaxFilterLogs.Add(new AjaxFilterLogs()
                {
                    UserId = UserData.Id.Value,
                    RoleGroupId = UserData.RoleGroupId,
                    AjaxId = AjaxFuncData.AjaxId,
                    PublicIp = UserPublicIP,
                    ClientIp = UserIP,
                    Para = Para,
                    Pass = true
                });
                base.OnActionExecuting(filterContext);
            }
            else
            {
                //當RoleScope沒有資料
                if (UserData.SuperAdmin && AjaxFuncData != null)
                {
                    //RoleScopr無資料
                    //但呼叫的Ajax Method有登錄在AjaxFunctionList內
                    //且身分為Super Admin
                    db.AjaxFilterLogs.Add(new AjaxFilterLogs()
                    {
                        UserId = UserData.Id.Value,
                        RoleGroupId = null,
                        AjaxId = AjaxFuncData.AjaxId,
                        PublicIp = UserPublicIP,
                        ClientIp = UserIP,
                        Para = Para,
                        Pass = true
                    });
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    //查無資料
                    //身分非Super Admin
                    //該操作非法或無任何權限
                    db.AjaxFilterLogs.Add(new AjaxFilterLogs()
                    {
                        UserId = UserData.Id.Value,
                        RoleGroupId = UserData.RoleGroupId,
                        AjaxId = AjaxFuncData == null ? -1 : AjaxFuncData.AjaxId,
                        PublicIp = UserPublicIP,
                        ClientIp = UserIP,
                        Para = Para,
                        Pass = false
                    });
                    filterContext.HttpContext.Response.StatusCode = 500;
                    //filterContext.Result = new RedirectToActionResult("Error", "Home", null);
                }
                db.SaveChanges();
            }
            
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
