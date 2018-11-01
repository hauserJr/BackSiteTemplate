using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class WhiteList
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Note { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
