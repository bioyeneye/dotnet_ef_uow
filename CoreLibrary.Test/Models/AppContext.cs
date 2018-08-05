using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLibrary.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Test.Models
{
    public class LibraryContext : EntityFrameworkDataContext
    {
        public LibraryContext(DbContextOptions<DbContext> options) : base(options)
        {
        }
    }
}
