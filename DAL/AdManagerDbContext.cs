using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oglasi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class AdManagerDbContext : IdentityDbContext<AppUser>
    {
        protected AdManagerDbContext() { }

        public AdManagerDbContext(DbContextOptions<AdManagerDbContext> options) : base(options) { }

        public DbSet<County> Counties { get; set; }
        public DbSet<AdCategory> AdCategories { get; set;}
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasData(new Post { 
                ID = 1, 
                PostDate = DateTime.Now.AddDays(-10), 
                Title="Alpha verzija online", Message="Alpha verzija aplikacije je online..."
            });
            modelBuilder.Entity<Post>().HasData(new Post { 
                ID = 2, 
                PostDate = DateTime.Now.AddDays(-5), 
                Title = "Beta verzija online", Message = "Beta većina značajki je funkcionalna" 
            });
            modelBuilder.Entity<Post>().HasData(new Post { 
                ID = 3, 
                PostDate = DateTime.Now, Title = "v1.01 online", 
                Message = "Prva release verzija online..." 
            });
        }
    }
}
