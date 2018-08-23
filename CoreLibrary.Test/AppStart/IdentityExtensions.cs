using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;

namespace CoreLibrary.Test.AppStart
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;

            return Convert.ToInt32(id);
        }
    }
}
