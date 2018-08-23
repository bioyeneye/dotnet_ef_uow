using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Test.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreLibrary.Test.Helper
{
    public class AppUtils
    {
        internal static IActionResult SignIn(ApplicationUser user, IList<string> roles)
        {
            var userResult = new { User = new { DisplayName = user.UserName, Roles = roles } };
            return new ObjectResult(userResult);
        }

    }
}
