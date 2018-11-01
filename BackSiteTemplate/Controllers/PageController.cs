using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BackSiteTemplate.Interface.IdentityServices;

namespace BackSiteTemplate.Controllers
{
    [Authorize]
    public class PageController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}