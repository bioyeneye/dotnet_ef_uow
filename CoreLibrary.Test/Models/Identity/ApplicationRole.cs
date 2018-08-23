using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoreLibrary.Test.Models.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        [StringLength(250)]
        public string Description { get; set; }

    }
}
