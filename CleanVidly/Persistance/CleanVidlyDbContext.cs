using System.Reflection;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NieuweStroom.POC.IT.Core.Entities;

namespace NieuweStroom.POC.IT.Persistance
{
    public class CleanVidlyDbContext : DbContext
    {

        public CleanVidlyDbContext(DbContextOptions<CleanVidlyDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}