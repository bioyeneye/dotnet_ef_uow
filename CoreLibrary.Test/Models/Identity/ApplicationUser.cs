using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CoreLibrary.Test.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool IsEnabled { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [Phone]
        public string Mobile { get; set; }

        [NotMapped]
        public string Name
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
        public ApplicationUserPhoto ProfilePhoto { get; set; }
    }
}
