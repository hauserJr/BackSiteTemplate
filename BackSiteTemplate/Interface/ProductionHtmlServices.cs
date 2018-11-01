using BackSiteTemplate.DB;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Routing;
using NavBarsContainHtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BackSiteTemplate.Interface
{
    public class ProductionHtmlServices
    {

        public interface IProductionHtmlAction
        {
            /// <summary>
            /// 取得Navbar
            /// </summary>
            /// <param name="CA">身分憑證</param>
            /// <param name="SuperAdmin">是否為超級使用者,預設false</param>
            /// <returns></returns>
            IHtmlContent GetNavbars(Guid? UserId, bool SuperAdmin, Guid RoleGroupId);
            IHtmlContent GetBreadcrumb(string ControllerName, string ActionName);

        }

        public class ProductionHtmlService : IProductionHtmlAction
        {
            private readonly BackSiteDBTempContext db;
            public ProductionHtmlService(BackSiteDBTempContext DB)
            {
                this.db = DB;
            }

            public IHtmlContent GetNavbars(Guid? UserId, bool SuperAdmin, Guid RoleGroupId)
            {
                string Result = string.Empty;
                List<NavBarsContain> NavbarQuery = new List<NavBarsContain>();
                if (SuperAdmin)
                {
                    NavbarQuery = this.db.MainNavs.Select(o => new NavBarsContain
                    {
                        MainNavsName = o.MainNavsName,
                        Sn = o.Sn,
                        SubNavsQuery = o.SubNavs.Select(x => new SubNavs {
                            SubNavsName = x.SubNavsName,
                            ControllerName = x.ControllerName,
                            ActionName = x.ActionName,
                            Sn = x.Sn
                        }).OrderBy(x => x.Sn).AsQueryable()
                    }).OrderBy(o => o.Sn).ToList();
                }
                else
                {
                    var RoleScopeData = this.db.RoleScope.Where(o => o.RoleGroupId == RoleGroupId).Select(o => new
                    {
                        SubNavsId = o.SubNavsId
                    }).ToList();
                    //取得使用者權限Guid
                    NavbarQuery = this.db.MainNavs.Select(o => new NavBarsContain
                    {
                        MainNavsName = o.MainNavsName,
                        Sn = o.Sn,
                        SubNavsQuery = o.SubNavs.Where(x => RoleScopeData.Where(_RoleScopeData => _RoleScopeData.SubNavsId == x.Id).Any())
                        .Select(x => new SubNavs
                        {
                            SubNavsName = x.SubNavsName,
                            ControllerName = x.ControllerName,
                            ActionName = x.ActionName,
                            Sn = x.Sn
                        }).OrderBy(x => x.Sn).AsQueryable()
                    }).OrderBy(o => o.Sn).ToList();

                }

                Result = ProductionNavBars(NavbarQuery);
                return new HtmlString(Result);
            }
            public IHtmlContent GetBreadcrumb(string ControllerName, string ActionName)
            {
                var result = this.db.SubNavs.Where(o => o.ControllerName == ControllerName && o.ActionName == ActionName).FirstOrDefault();

                if (result == null)
                {
                    return new HtmlString("");
                }
                else
                {
                    var Title = this.db.MainNavs.Where(o => o.Id == result.MainNavsId).FirstOrDefault().MainNavsName;
                    var Item = result.SubNavsName;
                    string HtmlTag = @"<ol class='breadcrumb'>
                                            <li class='breadcrumb-item'><a class='text-deepblue'>{0}</a></li>
                                            <li class='breadcrumb-item active' aria-current='page'>{1}</li>
                                        </ol>";
                    HtmlTag = string.Format(HtmlTag, Title, Item);
                    return new HtmlString(HtmlTag);
                }
            }
                

            private string ProductionNavBars(List<NavBarsContain> query)
            {
                //回傳字串
                string ReturnHTML = string.Empty;
                
                //Navbars string Template
                string HTMLtmp_li = @"<li class='nav-item dropdown'>{0}</li>";
                string HTMLtmp_MainNavsName = @"<a class='nav-link dropdown-toggle text-light' href='#' id='navbarDropdown' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>{0}</a> ";

                string HTMLtmp_SubNavsDiv = @"<div class='dropdown-menu' aria-labelledby='navbarDropdown'>
                                                   {0}
                                                </div>";
                string HTMLtmp_SubNavsName = @"<a class='dropdown-item' href='{1}'>{0}</a>";

                //動態存放產生的Navbar
                string NavsTitle = string.Empty;
                string NavsItem = string.Empty;
                foreach (var title in query.Where(o => o.SubNavsQuery.Any()))
                {
                    NavsTitle = HTMLtmp_MainNavsName;
                    NavsTitle = string.Format(NavsTitle,title.MainNavsName);
                   
                    foreach (var NavCase in title.SubNavsQuery)
                    {
                        NavsItem += HTMLtmp_SubNavsName;
                        //組合路徑
                        string RedirectURL = string.Format("/{0}/{1}", NavCase.ControllerName , NavCase.ActionName);
                        
                        NavsItem = string.Format(NavsItem,NavCase.SubNavsName,RedirectURL);
                    }
                    //將NavsItem加入Div中
                    NavsItem = string.Format(HTMLtmp_SubNavsDiv , NavsItem);

                    //將Title,Case存放進回傳字串中
                    ReturnHTML += string.Format(HTMLtmp_li, NavsTitle + NavsItem);

                    //重設Case
                    NavsItem = string.Empty;
                }

                return ReturnHTML;
            }
        }
    }
}
