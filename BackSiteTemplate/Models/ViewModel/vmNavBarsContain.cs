using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models.ViewModel
{
    public class vmNavBarsContain
    {
        public string MainNavsName { get; set; }
        public int? Sn { get; set; }

        public List<vmSubNavs> vmSubNavsQuery { get; set; }
    }

    public partial class vmSubNavs
    {
        private bool defSelected = false;
        public int Id { get; set; }
        public string SubNavsName { get; set; }
        public int? Sn { get; set; }

        public bool selected {
            get
            {
                return this.defSelected;
            }
            set
            {
                this.defSelected = value;
            }
        }
    }
}
