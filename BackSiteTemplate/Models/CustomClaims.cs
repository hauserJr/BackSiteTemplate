using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaimsList
{
    public class CustomClaimsValue
    {
        private Guid FakeGuid = Guid.NewGuid();
        private string _SuperAdmin = string.Empty;
        /// <summary>
        /// 使用者辨識值
        /// </summary>
        public Guid? Id
        {
            get; set;
        }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name
        {
            get;set;
        }

        /// <summary>
        /// 使用者是否為超級管理員
        /// </summary>
        public bool SuperAdmin
        {
            get { return bool.Parse(_SuperAdmin); }
            set { _SuperAdmin = value.ToString(); }
        }

        public Guid RoleGroupId
        {
            get
            {
                return FakeGuid;
            }
            set
            {
                FakeGuid = value;
            }
        }
    }
}
