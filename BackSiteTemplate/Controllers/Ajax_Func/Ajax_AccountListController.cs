using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BackSiteTemplate.Interface.AccountServices;
using static BackSiteTemplate.Interface.IdentityServices;

namespace BackSiteTemplate.Controllers.Func
{
    [Authorize]
    [AjaxFilter]
    public class Ajax_AccountListController : Controller
    {
        private IAccountAction AccountAction;
        private IIdentityAction UserData;
        public Ajax_AccountListController(IAccountAction _AccountAction, IIdentityAction _UserData)
        {
            this.AccountAction = _AccountAction;
            this.UserData = _UserData;
        }

        /// <summary>
        /// 取得帳號清單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAccountData(Guid? UserId)
        {
            var CreatorId = this.UserData.GetClaim(User.Identity).Id;
            var QueryData = this.AccountAction.GetAccountData(CreatorId, UserId);
            return PartialView("_GetPageAccountList", QueryData);
        }

        /// <summary>
        /// 儲存帳號設定
        /// </summary>
        /// <param name="AccountData"></param>
        /// <param name="RoleGroupId"></param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveUserAccount(AccountList AccountData, AccountListQueryData Data)
        {
            var CreatorId = this.UserData.GetClaim(User.Identity).Id;
            return this.AccountAction.SaveUserAccount(AccountData, CreatorId, Data);
        }

        /// <summary>
        /// 確認帳號是否重複
        /// </summary>
        /// <param name="LoginAccount">帳號</param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckLoginAccount(string LoginAccount)
        {
            return this.AccountAction.CheckLoginAccount(LoginAccount);
        }

        /// <summary>
        /// 取得用戶基本資料(暫時沒用到)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetUserBaseData(Guid UserId)
        {
            return this.AccountAction.GetUserBaseData(UserId);
        }

        /// <summary>
        /// 重置使用者密碼
        /// </summary>
        /// <param name="UserId">使用者ID</param>
        /// <returns></returns>
        [HttpPost]
        public string ResetPwd(Guid UserId)
        {
            var CreatorId = this.UserData.GetClaim(User.Identity).Id;
            return this.AccountAction.ResetPwd(UserId, CreatorId.Value);
        }

        /// <summary>
        /// 回傳DataTables所需的資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPageAccountList(DtsParameterSide<DtsAccountListData> _DtsParameterSide)
        {
            var DataJson = this.AccountAction.GetPageAccountList<DtsAccountListData>(_DtsParameterSide, Request.Query);
            
            return Content(DataJson);
        }

        /// <summary>
        /// 取得錯誤次數上限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetErrorTimesData()
        {
            var ErrorTimes = this.AccountAction.GetErrorTimesData();
            ViewBag.ErrorTimes = ErrorTimes;
            return PartialView("_GetErrorTimesData", ErrorTimes);
        }

        /// <summary>
        /// 修改錯誤次數上限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveErrorTimes(int ErrorTime)
        {
            var CreatorId = this.UserData.GetClaim(User.Identity).Id;
            this.AccountAction.SaveErrorTimes(ErrorTime, CreatorId);
            return Ok();
        }
    }
}