using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models
{
    public class AccountListQueryData
    {
        public Guid? RoleGroupId { get; set; }
        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public int AccountEnable { get; set; }
        public int OnLock { get; set; }
    }
}
