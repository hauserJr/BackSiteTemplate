using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models.ViewModel
{
    public class vmAccountData
    {
        private Guid FakeRoleGroupId = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LoginAccount { get; set; }
        public Guid RoleGroupId {
            get
            {
                return FakeRoleGroupId;
            }
            set
            {
                FakeRoleGroupId = value;
            }
        }

        public int AccountEnable { get; set; }

        public int OnLock { get; set; }

        public List<SelectListItem> RoleGroupSelectListItem { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> UserEnableSelectListItem { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> UserLockSelectListItem { get; set; } = new List<SelectListItem>();
    }

}
