using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccessEF
{
    public class ApiDemoContext : DbContext
    {
        public ApiDemoContext(DbContextOptions<ApiDemoContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
