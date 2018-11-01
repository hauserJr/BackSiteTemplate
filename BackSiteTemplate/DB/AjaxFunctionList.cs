using System;
using System.Collections.Generic;

namespace BackSiteTemplate.DB
{
    public partial class AjaxFunctionList
    {
        public int Id { get; set; }
        public int SubNavsId { get; set; }
        public string AjaxController { get; set; }
        public string AjaxAction { get; set; }
        public string Note { get; set; }
    }
}
