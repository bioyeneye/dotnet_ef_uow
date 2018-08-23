using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.Test.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLibrary.Test.Controllers.Api
{
    [Authorize]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BaseController : Controller
    {
        public BaseController()
        {
        }
    }
}
