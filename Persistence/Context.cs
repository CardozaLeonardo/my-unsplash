using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public Context(DbContextOptions<Context> opt): base(opt)
        {

        }
    }
}
