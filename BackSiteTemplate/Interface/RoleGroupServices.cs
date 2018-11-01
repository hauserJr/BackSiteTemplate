using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using BackSiteTemplate.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackSiteTemplate.Interface
{
    public class RoleGroupServices
    {

        public interface IRoleGroupAction
        {
            /// <summary>
            /// 新增權限群組
            /// </summary>
            /// <param name="_RoleGroupData">群組資料</param>
            /// <param name="SubNavsId">選單ID</param>
            /// <param name="UserId">創建者ID</param>
            /// <returns></returns>
            bool AddNewGroup(RoleGroup _RoleGroupData, string[] SubNavsId, Guid? UserId);

            /// <summary>
            /// 修改權限群組
            /// </summary>
            /// <param name="SubNavsId">選單ID</param>
            /// <param name="UserId">創建者ID</param>
            /// <param name="RoleGroupId">群組ID</param> 
            /// <returns></returns>
            bool EditExistingRoleGroup(RoleGroup _RoleGroupData, string[] SubNavsId, Guid? UserId);

            /// <summary>
            /// 取得權限群組頁面資料
            /// </summary>
            /// <param name="SuperAdmin">是否為超級使用者(Dev)</param>
            /// <param name="RoleGroupId">欲編輯的群組Id</param>
            /// /// <param name="EditorRoleGroupId">編輯者群組Id</param>
            /// <returns></returns>
            List<vmNavBarsContain> GetPageCheckBoxList(bool SuperAdmin, Guid? RoleGroupId, Guid EditorRoleGroupId);

            /// <summary>
            /// 取得DataTabls顯示的資料,包含排序/查詢/換頁皆在這裡處理
            /// </summary>
            /// <param name="RequestQuery"></param>
            /// <param name="_DtsParameterSide"></param>
            /// <returns></returns>
            string GetRoleGroupData<T>(IQueryCollection RequestQuery, DtsParameterSide<T> _DtsParameterSide) where T : class, new();

            /// <summary>
            /// 刪除權限
            /// </summary>
            /// <param name="Id">權限Id</param>
            /// <returns></returns>
            bool DeleteRoleGroup(Guid Id);

            /// <summary>
            /// 判斷群組名稱是否重複出現
            /// </summary>
            /// <param name="RoleGroupId">權限群組Id</param>
            /// <param name="GroupName">權限名稱</param>
            /// <returns></returns>
            bool CheckRoleGroupNameIsRepeat(Guid? RoleGroupId, string GroupName);
        }

        public class RoleGroupService : IRoleGroupAction
        {
            private readonly BackSiteDBTempContext db;
            public RoleGroupService(BackSiteDBTempContext DB)
            {
                this.db = DB;
            }

            /// <summary>
            /// 新增群組
            /// </summary>
            /// <param name="_RoleGroupData"></param>
            /// <param name="SubNavsId"></param>
            /// <param name="UserId"></param>
            /// <returns></returns>
            public bool AddNewGroup(RoleGroup _RoleGroupData, string[] SubNavsId, Guid? UserId)
            {
                Guid RoleGroupId = Guid.NewGuid();
                List<RoleScope> _RoleScope = new List<RoleScope>();
                //設定群組資料
                _RoleGroupData.Id = RoleGroupId;
                _RoleGroupData.Creator = UserId.Value;
                this.db.Set<RoleGroup>().Add(_RoleGroupData);

                //頁面綁定群組
                foreach (var id in SubNavsId)
                {
                    var query = this.db.SubNavs.Where(o => o.Id == int.Parse(id)).Any();
                    if (query)
                    {
                        _RoleScope.Add(new RoleScope()
                        {
                            RoleGroupId = RoleGroupId,
                            SubNavsId = int.Parse(id),
                            Creator = UserId.Value
                        });
                    }
                }
                this.db.RoleScope.AddRange(_RoleScope);
                this.db.SaveChanges();
                return true;
            }

            /// <summary>
            /// 編輯群組
            /// </summary>
            /// <param name="_RoleGroupData"></param>
            /// <param name="SubNavsId"></param>
            /// <param name="UserId"></param>
            /// <returns></returns>
            public bool EditExistingRoleGroup(RoleGroup _RoleGroupData,string[] SubNavsId, Guid? UserId)
            {
                List<RoleScope> _RoleScope = new List<RoleScope>();

                //取出該群組所有資料
                var Query = this.db.RoleScope.Where(o => o.RoleGroupId == _RoleGroupData.Id);

                //刪除需移除的資料
                var DeleteQuery = Query.Where(o => !SubNavsId.Contains(o.SubNavsId.ToString()));
                this.db.RoleScope.RemoveRange(DeleteQuery);

                //新增未存在的資料
                foreach (var checkData in SubNavsId)
                {
                    var checkQuery = Query.Where(o => o.SubNavsId == int.Parse(checkData)).Any();
                    if (!checkQuery)
                    {
                        _RoleScope.Add(new RoleScope()
                        {
                            RoleGroupId = _RoleGroupData.Id,
                            SubNavsId = int.Parse(checkData),
                            Creator = UserId.Value
                        });
                    }
                }

                //修改群組名稱
                var UpdateData = this.db.RoleGroup.Where(o => o.Id == _RoleGroupData.Id).Select(o => new RoleGroup
                {
                    Id = o.Id,
                    GroupName = _RoleGroupData.GroupName,
                    CreateDate = DateTime.Now,
                    Creator = UserId.Value
                }).FirstOrDefault();
                if (UpdateData != null)
                {
                    this.db.Attach(UpdateData);
                    var UpdateDataEntry = this.db.Entry(UpdateData);
                    UpdateDataEntry.Property(o => o.GroupName).IsModified = true;
                    UpdateDataEntry.Property(o => o.CreateDate).IsModified = true;
                    UpdateDataEntry.Property(o => o.Creator).IsModified = true;
                }
                
                //修改權限
                if (_RoleScope != null)
                {
                    this.db.RoleScope.AddRange(_RoleScope);
                }
                this.db.SaveChanges();
                return true;
            }

            /// <summary>
            /// 取得權限群組頁面資料
            /// </summary>
            /// <param name="SuperAdmin">是否為超級使用者(Dev)</param>
            /// <param name="RoleGroupId">欲編輯的群組Id</param>
            /// /// <param name="EditorRoleGroupId">編輯者群組Id</param>
            /// <returns></returns>
            public List<vmNavBarsContain> GetPageCheckBoxList(bool SuperAdmin, Guid? RoleGroupId, Guid EditorRoleGroupId)
            {
                List<vmNavBarsContain> NavbarQuery = new List<vmNavBarsContain>();
                if (SuperAdmin)
                {
                    NavbarQuery = this.db.MainNavs.Select(o => new vmNavBarsContain
                    {
                        MainNavsName = o.MainNavsName,
                        Sn = o.Sn,
                        vmSubNavsQuery = o.SubNavs.Select(x => new vmSubNavs
                        {
                            Id = x.Id,
                            SubNavsName = x.SubNavsName,
                            Sn = x.Sn
                        }).OrderBy(x => x.Sn).ToList()
                    }).OrderBy(o => o.Sn).ToList();
                }
                else
                {
                    var RoleScopeData = this.db.RoleScope.Where(o => o.RoleGroupId == EditorRoleGroupId).Select(o => new
                    {
                        SubNavsId = o.SubNavsId
                    }).ToList();
                    //取得使用者權限Guid
                    NavbarQuery = this.db.MainNavs.Select(o => new vmNavBarsContain
                    {
                        MainNavsName = o.MainNavsName,
                        Sn = o.Sn,
                        vmSubNavsQuery = o.SubNavs.Where(x => RoleScopeData.Where(_RoleScopeData => _RoleScopeData.SubNavsId == x.Id).Any())
                        .Select(x => new vmSubNavs
                        {
                            Id = x.Id,
                            SubNavsName = x.SubNavsName,
                            Sn = x.Sn
                        }).OrderBy(x => x.Sn).ToList()
                    }).OrderBy(o => o.Sn).ToList();
                }
                //檢查已勾選項目
                if (RoleGroupId.HasValue)
                {
                    var GetScopeList = this.db.RoleScope.Where(o => o.RoleGroupId == RoleGroupId).Select(o => new
                    {
                        o.SubNavsId
                    }).ToList();
                    foreach (var ScopeData in GetScopeList)
                    {
                        foreach (var NavsData in NavbarQuery)
                        {
                            var query = NavsData.vmSubNavsQuery.Where(o => o.Id == ScopeData.SubNavsId).FirstOrDefault();
                            query.selected = true;
                        }
                    }
                }
                return NavbarQuery;
            }

            /// <summary>
            /// 取得DataTabls顯示的資料,包含排序/查詢/換頁皆在這裡處理
            /// </summary>
            /// <param name="RequestQuery"></param>
            /// <param name="_DtsParameterSide"></param>
            /// <returns></returns>
            public string GetRoleGroupData<T>(IQueryCollection RequestQuery, DtsParameterSide<T> _DtsParameterSide) where T : class, new ()
            {
                int SkipRow = int.Parse(_DtsParameterSide.start);
                int TakeRow = int.Parse(_DtsParameterSide.length);

                var FilterStr = GetFormQuery(RequestQuery, "search[value]");

                var SortingIndex = int.Parse(GetFormQuery(RequestQuery, "order[0][column]"));
                var SortingType = GetFormQuery(RequestQuery, "order[0][dir]");

                string InfoTotalCount = string.Empty;

                //"群組名稱" => 自訂
                string[] SortingColumns = new string[] { "GroupName" };

                //取得對應資料 => 自訂
                var data = this.db.Set<RoleGroup>().AsNoTracking().Select(o => new DtsRoleGroupData()
                {
                    Id = o.Id,
                    GroupName = o.GroupName
                }); ;

                //過濾條件 => 自訂
                data = data.Where(o => o.GroupName.Contains(FilterStr));

                //依照條件排序
                data = data.OrderBy(SortingColumns[SortingIndex] + " " + SortingType);

                //取得輸出資料總數
                InfoTotalCount = data.Count().ToString();

                //取得要輸出到頁面的資料
                data = data.Skip(SkipRow).Take(TakeRow);

                _DtsParameterSide.data.AddRange(data as IEnumerable<T>);
                _DtsParameterSide.recordsTotal = InfoTotalCount;
                _DtsParameterSide.recordsFiltered = InfoTotalCount;

                //To Json
                var DataJson = JsonConvert.SerializeObject(_DtsParameterSide, Formatting.Indented);
                return DataJson;
            }

            /// <summary>
            /// 刪除權限
            /// </summary>
            /// <param name="Id">權限Id</param>
            /// <returns></returns>
            public bool DeleteRoleGroup(Guid Id)
            {
                try
                {
                    var query = this.db.RoleGroup.Where(o => o.Id == Id).FirstOrDefault();
                    if (query != null)
                    {
                        this.db.RoleGroup.Remove(query);
                        this.db.SaveChanges();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 判斷群組名稱是否重複出現
            /// </summary>
            /// <param name="RoleGroupId">權限群組Id</param>
            /// <param name="GroupName">權限名稱</param>
            /// <returns></returns>
            public bool CheckRoleGroupNameIsRepeat(Guid? RoleGroupId, string GroupName)
            {
                var Query = this.db.RoleGroup.Where(o => o.GroupName == GroupName);
                if (Query.Any() && RoleGroupId.HasValue)
                {
                    var Result = Query.Where(o => o.Id == RoleGroupId).Any();
                    if (Result)
                    {
                        //ID 與 名稱 一致
                        return false;
                    }
                    else
                    {
                        //ID 與 名稱 不一致
                        return true;
                    }
                }
                return Query.Any();
            }
        }

        /// <summary>
        /// 解析DataTables Request
        /// </summary>
        /// <param name="query"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetFormQuery(IQueryCollection query, string key)
        {
            var searchValue = query.Select(o => new
            {
                o.Key,
                o.Value
            }).ToDictionary(o => o.Key)[key].Value;
            return searchValue;
        }
    }
}
