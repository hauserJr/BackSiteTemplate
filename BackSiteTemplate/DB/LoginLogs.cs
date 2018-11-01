using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class LoginLogs
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
