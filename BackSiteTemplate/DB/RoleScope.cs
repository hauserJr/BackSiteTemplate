using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class RoleScope
    {
        public int Id { get; set; }
        public Guid RoleGroupId { get; set; }
        public int SubNavsId { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreateDate { get; set; }

        public RoleGroup RoleGroup { get; set; }
        public SubNavs SubNavs { get; set; }
    }
}
