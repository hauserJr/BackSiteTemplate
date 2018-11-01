using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models
{
    public enum EnumChangePwd : int
    {
        密碼錯誤 = 1,
        新密碼前後不一致 = 2,
        更新成功 = 3
    }
}
