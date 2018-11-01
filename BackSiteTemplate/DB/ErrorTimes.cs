using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class ErrorTimes
    {
        public int Id { get; set; }
        public int ErrorTimes1 { get; set; }
        public Guid Creator { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
