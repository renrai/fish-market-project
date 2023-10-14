using FishMarketProjectData.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectData.Database
{
    public class FishMarketContextDb : DbContext
    {
        public FishMarketContextDb(DbContextOptions<FishMarketContextDb> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(build =>
            {
                build.ToTable("Users");
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Email).HasMaxLength(100);
            });
        }
    }
}
