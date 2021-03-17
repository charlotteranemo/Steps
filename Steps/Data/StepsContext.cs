using Microsoft.EntityFrameworkCore;
using Steps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steps.Data
{
    public class StepsContext : DbContext
    {
        public StepsContext(DbContextOptions<StepsContext> options) : base(options)
        {

        }

        public DbSet<Fitspo> Steps_Fitspos { get; set; }
        public DbSet<Login> Steps_Login { get; set; }
        public DbSet<Email> Steps_Emails { get; set; }
    }
}
