using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class RoleGroup
    {
        public RoleGroup()
        {
            RoleScope = new HashSet<RoleScope>();
            UserRoleList = new HashSet<UserRoleList>();
        }

        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public int Sn { get; set; }
        public bool? GroupEnable { get; set; }
        public Guid Creator { get; set; }
        public DateTime? CreateDate { get; set; }

        public ICollection<RoleScope> RoleScope { get; set; }
        public ICollection<UserRoleList> UserRoleList { get; set; }
    }
}
