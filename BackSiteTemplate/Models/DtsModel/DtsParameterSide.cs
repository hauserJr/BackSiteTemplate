using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models
{
    public class DtsParameterSide<T>
    {
        public string draw { get; set; }

        /// <summary>
        /// 總筆數
        /// Total
        /// </summary>
        public string recordsTotal { get; set; }

        /// <summary>
        /// 條件篩選後的筆數
        /// Filter After Row Amounts
        /// </summary>
        public string recordsFiltered { get; set; }

        /// <summary>
        /// 開始頁數
        /// </summary>
        public string start { get; set; }

        /// <summary>
        /// 一頁呈現的筆數
        /// </summary>
        public string length { get; set; }

        public List<T> data { get; set; } = new List<T>();
    }
}
