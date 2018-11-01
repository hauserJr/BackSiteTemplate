using BackSiteTemplate.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NavBarsContainHtml
{
    public class NavBarsContain
    {
        public string MainNavsName { get; set; }
        public int? Sn { get; set; }

        public IQueryable<SubNavs> SubNavsQuery { get; set; }
    }
}
