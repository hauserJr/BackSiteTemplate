using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models
{
    public class ReturnCode
    {
        /// <summary>
        /// 回傳至前端狀態,false = 執行失敗,true = 執行成功
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 回傳至前端的字串
        /// </summary>
        public string Code { get; set; }
    }
}
