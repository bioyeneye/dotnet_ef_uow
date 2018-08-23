using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibrary.Test.Models.Identity
{
    public class ApplicationUserPhoto
    {
        [Key]
        public int Id { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
