using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BackSiteTemplate.Interface.AccountServices;

namespace BackSiteTemplate.Controllers
{
    [Authorize]
    [PageFilter]
    public class AccountListController : BaseController
    {
        private IAccountAction AccountAction;
        public AccountListController(IAccountAction _AccountAction)
        {
            this.AccountAction = _AccountAction;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AccountListManager()
        {

            //順便帶前端需要的特別資訊
            ViewBag.ErrorTimes = this.AccountAction.GetErrorTimesData();
            return View();
        }
    }
}