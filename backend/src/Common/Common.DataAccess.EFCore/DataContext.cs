using Common.DataAccess.EFCore.Configuration;
using Common.DataAccess.EFCore.Configuration.System;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Common.DataAccess.EFCore
{
    public class DataContext : DbContext
    {
        public ContextSession Session { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<HotspotResult> HotspotResults { get; set; }
        public DbSet<WonCode> WonCodes { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupEmail> GroupEmails { get; set; }

        public DataContext() { 
        
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Common.WebApiCore"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();
            var connectionString = configuration.GetConnectionString("localDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new UserClaimConfig());
            modelBuilder.ApplyConfiguration(new UserPhotoConfig());
            modelBuilder.ApplyConfiguration(new SettingsConfig());

            modelBuilder.HasDefaultSchema("lottery");

            modelBuilder.Entity<HotspotResult>(entity => {
                entity.HasMany(e => e.WonCodes)
                       .WithOne(e => e.HotspotResult)
                       .HasForeignKey(e => e.HotspotResultId)
                       .OnDelete(DeleteBehavior.Cascade);            
            });

            modelBuilder.Entity<Code>(entity => {
                entity.HasMany(e => e.WonCodes)
                       .WithOne(e => e.Code)
                       .HasForeignKey(e => e.CodeId)
                       .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
