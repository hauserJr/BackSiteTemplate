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
    public class WhiteListServices
    {

        public interface IWhiteListAction
        {
            /// <summary>
            /// 檢查白名單IP是否重複
            /// </summary>
            /// <param name="WhiteListIP">欲加入白名單的IP</param>
            /// <returns></returns>
            bool CheckWhiteList(string [] WhiteListIP, int? Id);


            /// <summary>
            /// 儲存白名單資料
            /// </summary>
            /// <param name="WhiteListIP">欲加入白名單的IP</param>
            /// <returns></returns>
            bool SaveWhiteList(string[] WhiteListIP, Guid Creator,int? Id,string Note = null);

            /// <summary>
            /// 取得前端所需資料
            /// </summary>
            /// <typeparam name="T">Model</typeparam>
            /// <param name="_DtsParameterSide"></param>
            /// <param name="RequestQuery"></param>
            /// <returns></returns>
            string GetDtsWhiteList<T>(DtsParameterSide<T> _DtsParameterSide, IQueryCollection RequestQuery) where T : class, new();

            /// <summary>
            /// 取得詳細的WhiteListData
            /// </summary>
            /// <param name="Id"></param>
            vmWhiteListData GetDetailWhiteListData(int Id);

            /// <summary>
            /// 刪除資料
            /// </summary>
            /// <param name="Id"></param>
            void DeleteData(int Id);
        }

        public class WhiteListService : IWhiteListAction
        {
            private readonly BackSiteDBTempContext db;
            public WhiteListService(BackSiteDBTempContext DB)
            {
                this.db = DB;
            }

            public bool SaveWhiteList(string[] WhiteListIP, Guid Creator, int? Id, string Note = null)
            {
                if (Id.HasValue)
                {
                    var Data = this.db.WhiteList.Where(o => o.Id == Id.Value).FirstOrDefault();
                    if (Data != null)
                    {
                        Data.Ip = string.Join(".", WhiteListIP);
                        Data.Note = Note;
                        Data.Creator = Creator;
                        Data.CreateDate = DateTime.Now;
                    }
                }
                else
                {
                    this.db.WhiteList.Add(new WhiteList()
                    {
                        Ip = string.Join(".", WhiteListIP),
                        Note = Note,
                        Creator = Creator
                    });

                }
                
                this.db.SaveChanges();
                return true;
            }

            /// <summary>
            /// 檢查白名單IP是否重複
            /// </summary>
            /// <param name="WhiteListIP">欲加入白名單的IP</param>
            /// <returns></returns>
            public bool CheckWhiteList(string[] WhiteListIP, int? Id)
            {
                bool Result = false;
                var IP = string.Join(".", WhiteListIP);
                if (!Id.HasValue)
                {
                    
                    Result = this.db.WhiteList.Where(o => o.Ip == IP).Any();
                }
                else
                {
                    var QueryId = this.db.WhiteList.Where(o => o.Ip == IP).Select(o => o.Id).FirstOrDefault();
                    Result = (QueryId == Id.Value);
                    return Result;
                }
                //false 回傳錯誤
                //true 正常執行
                Result = !Result;
                return Result;
            }

            public string GetDtsWhiteList<T>(DtsParameterSide<T> _DtsParameterSide, IQueryCollection RequestQuery) where T : class, new()
            {
                int SkipRow = int.Parse(_DtsParameterSide.start);
                int TakeRow = int.Parse(_DtsParameterSide.length);

                var FilterStr = GetFormQuery(RequestQuery, "search[value]");

                var SortingIndex = int.Parse(GetFormQuery(RequestQuery, "order[0][column]"));
                var SortingType = GetFormQuery(RequestQuery, "order[0][dir]");

                string InfoTotalCount = string.Empty;

                //  => 自訂
                string[] SortingColumns = new string[] { "Ip", "Note" };

                //取得對應資料  => 自訂 
                var data = this.db.WhiteList.Select(o => new DtsWhiteListData
                {
                    Id = o.Id,
                    Ip = o.Ip,
                    Note = o.Note,
                }).AsNoTracking();

                //過濾條件  => 自訂
                data = data.Where(o => o.Ip.Contains(FilterStr) || o.Note.Contains(FilterStr));

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

            public vmWhiteListData GetDetailWhiteListData(int Id)
            {

                return this.db.WhiteList.Where(o => o.Id == Id).Select(o => new vmWhiteListData()
                {
                    Id = o.Id,
                    Ip = o.Ip,
                    Note = o.Note
                }).FirstOrDefault();
            }

            public void DeleteData(int Id)
            {
                this.db.WhiteList.Remove(this.db.WhiteList.Where(o => o.Id == Id).FirstOrDefault());
                this.db.SaveChanges();
                
            }
        }

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
