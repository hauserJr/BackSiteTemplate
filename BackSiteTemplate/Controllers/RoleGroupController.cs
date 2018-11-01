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
    public class RoleGroupController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult RoleGroupManager()
        {
            return View();
        }
    }
}