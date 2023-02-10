using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Hashing
{
    public  class UserDb:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=OLASILE\SQLEXPRESS;Initial Catalog=UserApp;Integrated Security=True;TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
    }
}
