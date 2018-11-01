using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackSiteTemplate.Controllers
{
    [Authorize]
    [PageFilter]
    public class WhiteListController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WhiteListManager()
        {
            return View();
        }
    }
}