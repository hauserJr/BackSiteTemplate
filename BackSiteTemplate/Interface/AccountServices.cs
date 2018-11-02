using BackSiteTemplate.DB;
using BackSiteTemplate.Models;
using BackSiteTemplate.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackSiteTemplate.Interface
{
    public class AccountServices
    {

        public interface IAccountAction
        {
            string GetHashPwd(string originalPwd);
            List<Claim> Login(string Account, string Pwd);

            /// <summary>
            /// 取得使用者資料清單
            /// </summary>
            /// <param name="Creator">建立者ID</param>
            /// <returns></returns>
            vmAccountData GetAccountData(Guid? Creator, Guid? UserId);

            /// <summary>
            /// 儲存使用者資料
            /// </summary>
            /// <param name="AccountData"></param>
            /// <param name="RoleGroupId"></param>
            /// <param name="Creator"></param>
            bool SaveUserAccount(AccountList AccountData, Guid? Creator, AccountListQueryData Data);

            /// <summary>
            /// 取得使用者頁面資料
            /// </summary>
            /// <typeparam name="T">data Model</typeparam>
            /// <param name="_DtsParameterSide">Dts Model</param>
            /// <param name="RequestQuery">Http Request</param>
            /// <returns></returns>
            string GetPageAccountList<T>(DtsParameterSide<T> _DtsParameterSide, IQueryCollection RequestQuery) where T : class, new();

            /// <summary>
            /// 確認帳號是否重複
            /// </summary>
            /// <param name="LoginAccount">帳號</param>
            /// <returns></returns>
            bool CheckLoginAccount(string LoginAccount);

            /// <summary>
            /// 取得使用者基本資料
            /// </summary>
            /// <param name="UserId">使用者ID</param>
            string GetUserBaseData(Guid? UserId);

            /// <summary>
            /// 確認帳號是否被鎖定或啟用或未設定任何群組
            /// </summary>
            /// <param name="Account"></param>
            string GetAccountMessage(string Account);

            /// <summary>
            /// 重置密碼
            /// </summary>
            /// <param name="UserId"></param>
            /// <param name="CreatorId"></param>
            /// <returns></returns>
            string ResetPwd(Guid UserId, Guid CreatorId);

            /// <summary>
            /// 產生隨機密碼,參數皆可不帶
            /// </summary>
            /// <param name="PassWordLength">密碼長度,若無設定預設為八個字元</param>
            /// <returns></returns>
            string ProducePasswWord(int? PassWordLength = 8);

            /// <summary>
            /// 取得錯誤登入鎖定次數
            /// </summary>
            /// <returns></returns>
            int GetErrorTimesData();

            /// <summary>
            /// 修改登入錯誤上限
            /// </summary>
            /// <param name="ErrorTime">錯誤次數</param>
            /// <param name="Creator">創建者ID</param>
            void SaveErrorTimes(int ErrorTime,Guid? Creator);

            /// <summary>
            /// 修改密碼
            /// </summary>
            /// <param name="OldPwd">舊密碼</param>
            /// <param name="NewPwd">新密碼</param>
            /// <param name="CheckPwd">確認密碼</param>
            /// <returns></returns>
            int ChangePwdAct(Guid UserId, vmChangePwd vmChangePwd);
        }

        public class AccountService : IAccountAction
        {
            private readonly BackSiteDBTempContext db;
            public AccountService(BackSiteDBTempContext DB)
            {
                this.db = DB;
            }

            /// <summary>
            /// 判斷是否登入成功
            /// </summary>
            /// <param name="Account">帳號</param>
            /// <param name="Pwd">密碼</param>
            /// <returns></returns>
            public List<Claim> Login(string Account, string Pwd)
            {
                var claims = new List<Claim>();
                var HashPwd = GetHashPwd(Pwd);
                var UserStatus = this.db.AccountList.Where(o => (o.LoginAccount == Account && o.LoginPwd == HashPwd) ).FirstOrDefault();
                var RoleGroupId = Guid.NewGuid();

                //
                var ErrorTimes = this.db.ErrorTimes.FirstOrDefault().ErrorTimes1;
                if (UserStatus != null)
                {
                    if (!UserStatus.SuperAdmin.Value)
                    {
                        RoleGroupId = this.db.UserRoleList.Where(o => o.UserId == UserStatus.Id).Select(o => o.RoleGroup.Id).FirstOrDefault();
                    }
                    if (ErrorTimes > 0 && UserStatus.ErrorTimes >= ErrorTimes)
                    {
                        UserStatus.OnLock = true;

                    }
                    else
                    {
                        claims = new List<Claim>
                        {
                            new Claim("Id", UserStatus.Id.ToString()),
                            new Claim("Name", UserStatus.UserName),
                            new Claim("RoleGroupId", RoleGroupId.ToString()),
                            new Claim("SuperAdmin", UserStatus.SuperAdmin.ToString()),
                        };
                    }
                }
                else
                {
                    //登入失敗
                    if (ErrorTimes > 0)
                    {
                        var AccountQuery = this.db.AccountList.Where(o => o.LoginAccount == Account).FirstOrDefault();
                        if (AccountQuery != null && AccountQuery.SuperAdmin.Value == false)
                        {
                            AccountQuery.ErrorTimes += 1;
                            if (AccountQuery.ErrorTimes >= ErrorTimes)
                            {
                                AccountQuery.OnLock = true;
                            }
                        }
                    }
                    
                }
                this.db.SaveChanges();
                return claims;
            }

            public string GetAccountMessage(string Account)
            {
                var Data = this.db.AccountList.Where(o => o.LoginAccount == Account && !o.SuperAdmin.Value).Select(o => new
                {
                    Enable = o.AccountEnable == false ? "該帳號尚未啟用，請聯絡系統管理員。" : "",
                    Lock = o.OnLock.Value ? "該帳號目前已被鎖定，請聯絡系統管理員。" : "",
                    HasRoleGroup = !o.UserRoleList.Any() ? "群組並未被正常設定，請聯絡系統管理員。" : ""
                }).FirstOrDefault();

                string Message = string.Empty;
                if (Data != null)
                {
                    if (!string.IsNullOrEmpty(Data.Enable))
                    {
                        Message = Data.Enable;
                    }
                    else if (!string.IsNullOrEmpty(Data.Lock))
                    {
                        Message = Data.Lock;
                    }
                    else
                    {
                        Message = Data.HasRoleGroup;
                    }
                }
                return Message;
            }
            /// <summary>
            /// 取得使用者資料清單
            /// </summary>
            /// <param name="UserId"></param>
            /// <returns></returns>
            public vmAccountData GetAccountData(Guid? Creator,Guid? UserId)
            {

                var Query = new vmAccountData();
                var RoleGroupList = this.db.RoleGroup.Select(o => new
                {
                    Id = o.Id,
                    GroupName = o.GroupName
                }).AsNoTracking();

                if (UserId.HasValue)
                {
                    //編輯
                    Query = this.db.AccountList.Where(o => o.Id == UserId).Select(o => new vmAccountData
                    {
                        UserId = o.Id,
                        UserName = o.UserName,
                        LoginAccount = o.LoginAccount,
                        AccountEnable = o.AccountEnable.Value ? 1 : 0,
                        OnLock = o.OnLock.Value ? 1 : 0,
                        RoleGroupId = o.UserRoleList.FirstOrDefault().RoleGroup.Id != null ? o.UserRoleList.FirstOrDefault().RoleGroup.Id : Guid.NewGuid(),

                    }).FirstOrDefault();

                    foreach (var RoleGroup in RoleGroupList)
                    {
                        Query.RoleGroupSelectListItem.Add(new SelectListItem()
                        {
                            Value = RoleGroup.Id.ToString(),
                            Text = RoleGroup.GroupName,
                            Selected = RoleGroup.Id == Query.RoleGroupId ? true : false
                        });
                    }

                    for (int i = 0;i <= 1; i++)
                    {
                        //帳號是否鎖定
                        Query.UserLockSelectListItem.Add(new SelectListItem()
                        {
                            Value = i.ToString(),
                            Text = i == 0 ? "正常" : "鎖定",
                            Selected = Query.OnLock == i
                        });

                        //帳號是否啟用
                        Query.UserEnableSelectListItem.Add(new SelectListItem()
                        {
                            Value = i.ToString(),
                            Text = i == 0 ? "未啟用" : "已啟用",
                            Selected = Query.AccountEnable == i
                        });
                    }
                }
                else
                {
                    //新增
                    foreach (var RoleGroup in RoleGroupList)
                    {
                        Query.RoleGroupSelectListItem.Add(new SelectListItem
                        {
                            Value = RoleGroup.Id.ToString(),
                            Text = RoleGroup.GroupName,
                            Selected = false
                        });
                    }
                }
                return Query;
            }

            /// <summary>
            /// 儲存使用者資料
            /// </summary>
            /// <param name="AccountData"></param>
            /// <param name="RoleGroupId"></param>
            /// <param name="Creator"></param>
            public bool SaveUserAccount(AccountList AccountData, Guid? Creator, AccountListQueryData Data)
            {
                if (Data.UserId.HasValue)
                {
                    var AccountQuery = this.db.AccountList.Find(Data.UserId);
                    AccountQuery.UserName = Data.UserName;
                    AccountQuery.AccountEnable = Data.AccountEnable == 1 ? true : false;
                    AccountQuery.OnLock = Data.OnLock == 1 ? true : false;
                    AccountQuery.Creator = Creator;
                    AccountData.CreateDate = DateTime.Now;

                    if (AccountQuery.OnLock.Value == false)
                    {
                        AccountQuery.ErrorTimes = 0;
                    }

                    //.RoleGroupId = Data.RoleGroupId.Value
                    var UserRoleListQuery = this.db.UserRoleList.Where(o => o.UserId == Data.UserId).FirstOrDefault();
                    if (UserRoleListQuery == null)
                    {
                        this.db.UserRoleList.Add(new UserRoleList()
                        {
                            UserId = Data.UserId.Value,
                            RoleGroupId = Data.RoleGroupId.Value,
                            Creator = Creator.Value
                        });
                    }
                    else
                    {
                        UserRoleListQuery.RoleGroupId = Data.RoleGroupId.Value;
                    }
                }
                else
                {
                    var CheckAccountResult = CheckLoginAccount(AccountData.LoginAccount);
                    if (CheckAccountResult)
                    {
                        return false;
                    }
                    else
                    {
                        var AccountNewGuid = Guid.NewGuid();
                        AccountData.Id = AccountNewGuid;
                        AccountData.LoginPwd = GetHashPwd(AccountData.LoginPwd);
                        AccountData.Creator = Creator;
                        this.db.AccountList.Add(AccountData);

                        this.db.UserRoleList.Add(new UserRoleList()
                        {
                            UserId = AccountNewGuid,
                            RoleGroupId = Data.RoleGroupId.Value,
                            Creator = Creator.Value
                        });
                    }
                }
                this.db.SaveChanges();
                return true;
            }

            /// <summary>
            /// 取得使用者頁面資料
            /// </summary>
            /// <typeparam name="T">data Model</typeparam>
            /// <param name="_DtsParameterSide">Dts Model</param>
            /// <param name="RequestQuery">Http Request</param>
            /// <returns></returns>
            public string GetPageAccountList<T>(DtsParameterSide<T> _DtsParameterSide, IQueryCollection RequestQuery) where T : class, new()
            {
                int SkipRow = int.Parse(_DtsParameterSide.start);
                int TakeRow = int.Parse(_DtsParameterSide.length);

                var FilterStr = GetFormQuery(RequestQuery, "search[value]");

                var SortingIndex = int.Parse(GetFormQuery(RequestQuery, "order[0][column]"));
                var SortingType = GetFormQuery(RequestQuery, "order[0][dir]");
                
                string InfoTotalCount = string.Empty;

                //"使用者名稱", "帳號", "使用者群組", "啟用狀態"  => 自訂
                string[] SortingColumns = new string[] { "UserName", "Account", "UserGroupName", "AccountEnable" };

                //取得對應資料  => 自訂 
                var data = this.db.AccountList.Where(o => !o.SuperAdmin.Value).Select(o => new DtsAccountListData
                {
                    UserId = o.Id,
                    UserName = o.UserName,
                    Account = o.LoginAccount,
                    AccountEnable = o.AccountEnable.ToString(),
                    UserGroupName = o.UserRoleList.Select(x => x.RoleGroup).FirstOrDefault().GroupName ?? "查無群組"
                }).AsNoTracking();

                //過濾條件  => 自訂
                data = data.Where(o => o.Account.Contains(FilterStr) || o.UserGroupName.Contains(FilterStr) || o.UserName.Contains(FilterStr) || o.AccountEnable.Contains(FilterStr));

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
            /// 確認帳號是否重複
            /// </summary>
            /// <param name="LoginAccount">帳號</param>
            /// <returns></returns>
            public bool CheckLoginAccount(string LoginAccount)
            {
                return this.db.AccountList.Where(o => o.LoginAccount == LoginAccount).Any();
            }

            /// <summary>
            /// 取得使用者基本資料
            /// </summary>
            /// <param name="UserId">使用者ID</param>
            public string GetUserBaseData(Guid? UserId)
            {
                var Result = this.db.AccountList.Where(o => o.Id == UserId).Select(o => new
                {
                    UserName = o.UserName,
                    LoginAccount = o.LoginAccount
                });

                var DataJson = JsonConvert.SerializeObject(Result, Formatting.Indented);
                return DataJson;
            }

            /// <summary>
            /// 重置使用者密碼
            /// </summary>
            /// <param name="UserId">使用者Id</param>
            public string ResetPwd(Guid UserId, Guid CreatorId)
            {
                string OriPwd = ProducePasswWord();
                var ResultData = this.db.AccountList.Find(UserId);
                ResultData.LoginPwd = GetHashPwd(OriPwd);
                ResultData.Creator = CreatorId;
                ResultData.CreateDate = DateTime.Now;
                this.db.AccountList.Update(ResultData);
                this.db.SaveChanges();
                return OriPwd;
            }

            public int ChangePwdAct(Guid UserId, vmChangePwd vmChangePwd)
            {
                int status = 0;
                var UserOriPassWord = GetHashPwd(vmChangePwd.OldPwd);

                var GetCheckData = this.db.AccountList.Find(UserId);
                if (GetCheckData.LoginPwd == UserOriPassWord)
                {
                    var UserNewPassWord = GetHashPwd(vmChangePwd.NewPwd);
                    GetCheckData.LoginPwd = UserNewPassWord;
                    this.db.SaveChanges();
                    status = (int)EnumChangePwd.更新成功;
                }
                else
                {
                    status = (int)EnumChangePwd.密碼錯誤;
                }

                return status;
            }

            /*************************************************************************************************************/
            /// <summary>
            /// 將密碼加密
            /// </summary>
            /// <param name="originalPwd">未加密前的密碼</param>
            /// <returns></returns>
            public string GetHashPwd(string originalPwd)
            {
                byte[] BytePwa = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(originalPwd)));
                var SaltPwd = AddSaltToPwd(BytePwa);
                return SaltPwd;
            }

            /// <summary>
            /// 產生隨機密碼,參數皆可不帶
            /// </summary>
            /// <param name="PassWordLength">密碼長度,若無設定預設為八個字元</param>
            /// <returns></returns>
            public string ProducePasswWord(int? PassWordLength = 8)
            {
                int UpASCIIStart = 65;
                int UpASCIIEnd = 90;
                int LowerASCIIStart = 97;
                int LowerASCIIEnd = 122;

                //Convert.ToChar(charPara).ToString()
                var UpperQuery = Enumerable.Range(1, 2).Select(o => Convert.ToChar(new Random().Next(UpASCIIStart, UpASCIIEnd)).ToString()).ToArray() as IEnumerable<string>;
                var LowerQuery = Enumerable.Range(1, 2).Select(o => Convert.ToChar(new Random().Next(LowerASCIIStart, LowerASCIIEnd)).ToString()).ToArray() as IEnumerable<string>;
                var NumQuery = Enumerable.Range(1, 4).Select(o => new Random().Next(0, 9).ToString()).ToArray() as IEnumerable<string>;
                var Query = new List<string>();
                Query.AddRange(UpperQuery);
                Query.AddRange(LowerQuery);
                Query.AddRange(NumQuery);

                //洗牌
                for (int index = 0; index <= 20; index++)
                {
                    var IndexAdrs = Enumerable.Range(1, 1).Select(o => new Random().Next(0, Query.Count() - 1)).FirstOrDefault();
                    var ArrayAdrs = Enumerable.Range(1, 1).Select(o => new Random().Next(0, Query.Count() - 1)).FirstOrDefault();
                    while (ArrayAdrs == IndexAdrs)
                    {
                        IndexAdrs = Enumerable.Range(1, 1).Select(o => new Random().Next(0, Query.Count() - 1)).FirstOrDefault();
                        ArrayAdrs = Enumerable.Range(1, 1).Select(o => new Random().Next(0, Query.Count() - 1)).FirstOrDefault();
                    }
                    string StayByA = Query[IndexAdrs];
                    string StayByB = Query[ArrayAdrs];

                    Query[IndexAdrs] = StayByB;
                    Query[ArrayAdrs] = StayByA;
                }

                //輸出
                string Result = string.Empty;
                foreach (var Item in Query)
                {
                    Result += Item;
                }
                return Result;
            }

            /// <summary>
            /// 查詢登入錯誤上限
            /// </summary>
            /// <returns></returns>
            public int GetErrorTimesData()
            {
                return this.db.ErrorTimes.FirstOrDefault().ErrorTimes1;
            }

            /// <summary>
            /// 修改登入錯誤上限
            /// </summary>
            /// <param name="ErrorTime">錯誤次數</param>
            /// <param name="Creator">創建者ID</param>
            public void SaveErrorTimes(int ErrorTime, Guid? Creator)
            {
                var query = this.db.ErrorTimes.FirstOrDefault();
                query.ErrorTimes1 = ErrorTime;
                query.Creator = Creator.Value;
                query.CreateDate = DateTime.Now;
                this.db.SaveChanges();
            }

            /// <summary>
            /// 密碼加密
            /// </summary>
            /// <param name="Pwd">原密碼</param>
            /// <returns></returns>
            private string AddSaltToPwd(Byte[] Pwd)
            {
                //位元組的隨機金鑰
                int Byte_cb = 8;

                //定義兩組SaltKey,必須大於8Byte
                byte[] SaltKey_1 = Encoding.UTF8.GetBytes("Judical_Gov-@2018");
                byte[] SaltKey_2 = Encoding.UTF8.GetBytes("~b@(0)@d~");

                //First Add Salt
                Rfc2898DeriveBytes Rfc_1 = new Rfc2898DeriveBytes(Pwd, SaltKey_1, 2);
                byte[] GetRfc_1 = Rfc_1.GetBytes(Byte_cb);
                var Salt_1Pwd = Convert.ToBase64String(GetRfc_1);

                //Second Add Salt
                Rfc2898DeriveBytes Rfc_2 = new Rfc2898DeriveBytes(Salt_1Pwd, SaltKey_2);
                byte[] GetRfc_2 = Rfc_2.GetBytes(Byte_cb);

                var Salt_2Pwd = Convert.ToBase64String(GetRfc_2);
                return Salt_2Pwd;
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
