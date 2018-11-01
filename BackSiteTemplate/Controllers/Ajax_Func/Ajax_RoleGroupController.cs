using System;
using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BackSiteTemplate.Interface.IdentityServices;
using static BackSiteTemplate.Interface.RoleGroupServices;

namespace BackSiteTemplate.Controllers
{
    [Authorize]
    [AjaxFilter]
    public class Ajax_RoleGroupController : Controller
    {
        BackSiteDBTempContext db;
        IIdentityAction UserData;
        IRoleGroupAction RoleGroupFunc;
        public Ajax_RoleGroupController(BackSiteDBTempContext DB, IIdentityAction _UserData, IRoleGroupAction _RoleGroupFunc)
        {
            this.db = DB;
            this.UserData = _UserData;
            this.RoleGroupFunc = _RoleGroupFunc;
        }

        /// <summary>
        /// 取得頁面權限清單(CheckBox)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetPageCheckBoxList(Guid? Id)
        {
            var SuperAdmin = this.UserData.GetClaim(User.Identity).SuperAdmin;
            var EditorRoleGroupId = this.UserData.GetClaim(User.Identity).RoleGroupId;
            if (Id.HasValue)
            {
                TempData["RoleGroupName"] = this.db.RoleGroup.Find(Id).GroupName;
                TempData["RoleGroupId"] = Id;
            }

            var Data = this.RoleGroupFunc.GetPageCheckBoxList(SuperAdmin, Id, EditorRoleGroupId);
            
            return PartialView("_GetPageCheckBoxList", Data);
        }

        /// <summary>
        /// 儲存資料
        /// </summary>
        /// <param name="_RoleGroupData"></param>
        /// <param name="SubNavsId"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveRoleGroup(RoleGroup _RoleGroupData,string [] SubNavsId, Guid? Id)
        {
            var UserData = this.UserData.GetClaim(User.Identity);
            var CheckQuery = CheckRoleGroupName(_RoleGroupData.Id, _RoleGroupData.GroupName);
            if (Id.HasValue && !CheckQuery)
            {
                this.RoleGroupFunc.EditExistingRoleGroup(_RoleGroupData, SubNavsId, UserData.Id);
            }
            else
            {
                this.RoleGroupFunc.AddNewGroup(_RoleGroupData, SubNavsId, UserData.Id);
            }
            
            return Ok();
        }

        /// <summary>
        /// 取得權限群組清單Json回傳至前端,由DataTables處理
        /// </summary>
        /// <param name="_DtsParameterSide"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoleGroupData(DtsParameterSide<DtsRoleGroupData> _DtsParameterSide)
        {
            var DataJson = this.RoleGroupFunc.GetRoleGroupData<DtsRoleGroupData>(Request.Query, _DtsParameterSide);
            return Content(DataJson);
        }

        /// <summary>
        /// 刪除權限
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteRoleGroup(Guid Id)
        {
            var DeleteResult = this.RoleGroupFunc.DeleteRoleGroup(Id);
            if (DeleteResult)
            {
                return Content("資料刪除成功.");
            }
            else
            {
                return Content("資料刪除失敗.");
            }
        }

        /// <summary>
        /// 判斷權限名稱是否重複出現
        /// </summary>
        /// <param name="RoleGroupId">群組ID</param>
        /// <param name="GroupName">群組名稱</param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckRoleGroupName(Guid? RoleGroupId,string GroupName)
        {
            return this.RoleGroupFunc.CheckRoleGroupNameIsRepeat(RoleGroupId, GroupName);
        }
    }
}