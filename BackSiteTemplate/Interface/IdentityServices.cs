using ClaimsList;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BackSiteTemplate.Interface
{
    public class IdentityServices
    {
        public interface IIdentityAction
        {
            CustomClaimsValue GetClaim(IIdentity identity);
        }
        public class IdentityService : IIdentityAction
        {
            //public IdentityService(Tkey _Tk, Tvalue _Tv)
            //{
            //}
            public CustomClaimsValue GetClaim(IIdentity identity)
            {
                SortedList<string, string> _list = new SortedList<string, string>();
                ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
                foreach (var item in claimsIdentity.Claims)
                {
                    _list.Add(item.Type, item.Value);
                }

                //利用SortedList 自建Key,Value後轉Json
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
                string ToJson = JsonConvert.SerializeObject(_list, jsonSerializerSettings);
                //再由Json To Custom Object
                var customClaimsValue = JsonConvert.DeserializeObject<CustomClaimsValue>(ToJson);
                return customClaimsValue;
            }
        }
    }
}
