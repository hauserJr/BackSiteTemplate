using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class MainNavs
    {
        public MainNavs()
        {
            SubNavs = new HashSet<SubNavs>();
        }

        public int Id { get; set; }
        public string MainNavsName { get; set; }
        public int? Sn { get; set; }

        public ICollection<SubNavs> SubNavs { get; set; }
    }
}
