using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using BotDetect.Web.Mvc;
using static BackSiteTemplate.Interface.AccountServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BackSiteTemplate.DB;
using static BackSiteTemplate.Interface.IdentityServices;
using BackSiteTemplate.Models.ViewModel;

namespace BackSiteTemplate.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAccountAction _IAccountAction;
        private IIdentityAction UserData;
        BackSiteDBTempContext db;

        public HomeController(IAccountAction IAccountAction, BackSiteDBTempContext DB, IIdentityAction _UserData)
        {
            this._IAccountAction = IAccountAction;
            this.db = DB;
            this.UserData = _UserData;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Page/Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string Account, string Pwd)
        {
            var LoginClaims = this._IAccountAction.Login(Account, Pwd);


            var GetAccountMessage = this._IAccountAction.GetAccountMessage(Account);
            string ErrorMessage = string.IsNullOrEmpty(GetAccountMessage) ? "" : GetAccountMessage;
            if (LoginClaims.Count() > 0)
            {
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(LoginClaims, CookieAuthenticationDefaults.AuthenticationScheme)),
                       new AuthenticationProperties()
                       {
                       //設定逾時登出時間
                       ExpiresUtc = DateTimeOffset.Now.AddMinutes(30)
                       });
                    this.db.LoginLogs.Add(new LoginLogs()
                    {
                        Account = Account,
                        LoginTime = DateTime.Now
                    });
                    this.db.SaveChanges();
                    return RedirectToAction("Index", "Page");
                }
            }
            else if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "帳號或密碼錯誤，如須查詢帳號或重置密碼請聯絡系統管理員。";
            }
           
            TempData["Account"] = Account;
            TempData["Pwd"] = Pwd;
            TempData["LoginMeg"] = ErrorMessage;
            return View("Index");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePwd()
        {

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePwd(vmChangePwd vmChangePwd)
        {
            if (ModelState.IsValid)
            {
                var UserData = this.UserData.GetClaim(User.Identity);
                var status = this._IAccountAction.ChangePwdAct(UserData.Id.Value, vmChangePwd);
                if (status == (int)EnumChangePwd.更新成功)
                {
                    HttpContext.SignOutAsync();
                    return Redirect("/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "登入嘗試失試。");
                    return View(vmChangePwd);
                }
            }
            return View(vmChangePwd);
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
