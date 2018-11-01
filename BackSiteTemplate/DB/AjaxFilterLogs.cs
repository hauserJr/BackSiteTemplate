using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class AjaxFilterLogs
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? RoleGroupId { get; set; }
        public int AjaxId { get; set; }
        public string PublicIp { get; set; }
        public string ClientIp { get; set; }
        public bool Pass { get; set; }
        public string Para { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
