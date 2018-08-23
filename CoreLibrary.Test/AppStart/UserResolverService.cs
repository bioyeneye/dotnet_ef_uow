using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CoreLibrary.Test.AppStart
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUser()
        {
            return _context.HttpContext?.User?.Identity?.Name;
        }
    }
}
