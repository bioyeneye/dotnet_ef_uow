using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibrary.Test.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
