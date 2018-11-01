using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class AccountList
    {
        public AccountList()
        {
            UserRoleList = new HashSet<UserRoleList>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string LoginAccount { get; set; }
        public string LoginPwd { get; set; }
        public int? Sn { get; set; }
        public int ErrorTimes { get; set; }
        public bool? OnLock { get; set; }
        public bool? SuperAdmin { get; set; }
        public bool? AccountEnable { get; set; }
        public Guid? Creator { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<UserRoleList> UserRoleList { get; set; }
    }
}
