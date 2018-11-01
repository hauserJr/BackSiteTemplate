using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackSiteTemplate.Lib
{
    public class GetIdentity
    {
        public static string Get<IIdentity>(IIdentity identity, string ClaimTypeName)
        {
            return GetClaimsValue(identity, ClaimTypeName);
        }

        private static string FindFirstValue(ClaimsIdentity identity, string claimType)
        {
            Claim _claim = identity.FindFirst(claimType);

            if (_claim != null)
            {
                return _claim.Value;
            }
            else
            {
                return null;
            }
        }

        private static string GetClaimsValue<IIdentity>(IIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }

            ClaimsIdentity _ClaimsIdentity = identity as ClaimsIdentity;

            if (_ClaimsIdentity != null)
            {
                return FindFirstValue(_ClaimsIdentity, claimType);
            }
            else
            {
                return null;
            }
        }
    }
}
