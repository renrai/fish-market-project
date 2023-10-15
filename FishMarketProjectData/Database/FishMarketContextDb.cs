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
        public DbSet<FishEntity> Fishs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(build =>
            {
                build.ToTable("Users");
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Email).HasMaxLength(100);
            });
            modelBuilder.Entity<FishEntity>(build =>
            {
                build.ToTable("Fishs");
                build.HasKey(entry => entry.Id);
                build.Property(entry => entry.Specie).HasMaxLength(256);
            });
        }
    }
}
