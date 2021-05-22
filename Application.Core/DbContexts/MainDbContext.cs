using Application.Core.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.DbContexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AdvertItem> AdvertItems { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<AdvertCategory> AdvertCategories { get; set; }
    }
}
