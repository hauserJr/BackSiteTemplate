using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models.ViewModel
{
    public class vmWhiteListData
    {
        public int Id { get; set;}

        public string Ip { get; set; }

        public string Note { get; set; }
    }

}
