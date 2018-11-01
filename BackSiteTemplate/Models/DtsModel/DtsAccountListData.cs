using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models
{
    public class DtsAccountListData
    {
        private string _ResultStr = string.Empty;
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }

        public string AccountEnable {
            get
            {
                return AccountEnableResult(_ResultStr);
            }
            set
            {
                _ResultStr = value;
            }
        }
        public string UserGroupName { get; set; }
        private string AccountEnableResult(string enable)
        {
            var Result = int.Parse(enable) == 1 ? "已啟用" : "未啟用";
            return Result;
        }
    }
}
