using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class UserRoleList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleGroupId { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreateDate { get; set; }

        public RoleGroup RoleGroup { get; set; }
        public AccountList User { get; set; }
    }
}
