using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BackSiteTemplate.Interface.IdentityServices;
using static BackSiteTemplate.Interface.WhiteListServices;

namespace BackSiteTemplate.Controllers.Ajax_Func
{
    [Authorize]
    [AjaxFilter]
    public class Ajax_WhiteListController : Controller
    {
        private IWhiteListAction WhiteList;
        private IIdentityAction UserData;
        public Ajax_WhiteListController(IWhiteListAction _WhiteList, IIdentityAction _UserData)
        {
            this.WhiteList = _WhiteList;
            this.UserData = _UserData;
        }
        /// <summary>
        /// 取得Pop Up Windows的資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetPageWhiteList(int? Id)
        {
            if (!Id.HasValue)
            {
                return PartialView("_GetPageWhiteList");
            }
            else
            {
                return PartialView("_GetPageWhiteList",this.WhiteList.GetDetailWhiteListData(Id.Value));
            }
        }

        [HttpPost]
        public IActionResult DeleteWhiteList(int? Id)
        {
            if (!Id.HasValue)
            {
                HttpContext.Response.StatusCode = 400;
                return View();
            }
            else
            {
                this.WhiteList.DeleteData(Id.Value);
                return Content("資料刪除成功.");
            }
        }
        /// <summary>
        /// 確認白名單設定IP是否重複
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckWhiteList(string[] WhiteListIP)
        {
            var Result = this.WhiteList.CheckWhiteList(WhiteListIP, null);
            return Content(Result.ToString());
        }

        [HttpPost]
        public IActionResult SaveWhiteList(string[] WhiteListIP,int? Id,string Note = null)
        {
            var UserDate = this.UserData.GetClaim(User.Identity);
            bool Result = false;
            if (this.WhiteList.CheckWhiteList(WhiteListIP, Id) == true)
            {
                Result = this.WhiteList.SaveWhiteList(WhiteListIP, UserDate.Id.Value, Id, Note);
            }

            var query = new
            {
                result = Result
            };
            return Json(query);
        }

        [HttpGet]
        public IActionResult GetDtsWhiteList(DtsParameterSide<DtsWhiteListData> _DtsParameterSide)
        {
            var DataJson = this.WhiteList.GetDtsWhiteList<DtsWhiteListData>(_DtsParameterSide, Request.Query);
            return Content(DataJson);
        }
    }
}