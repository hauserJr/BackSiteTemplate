using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class SubNavs
    {
        public SubNavs()
        {
            RoleScope = new HashSet<RoleScope>();
        }

        public int Id { get; set; }
        public int MainNavsId { get; set; }
        public string SubNavsName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? Sn { get; set; }

        public MainNavs MainNavs { get; set; }
        public ICollection<RoleScope> RoleScope { get; set; }
    }
}
