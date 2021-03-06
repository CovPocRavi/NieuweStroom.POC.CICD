using System.Reflection;
using NieuweStroom.POC.IT.Core.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace NieuweStroom.POC.IT.Persistance
{
    public class NieuweStroomPocDbContext : DbContext
    {

        public NieuweStroomPocDbContext(DbContextOptions<NieuweStroomPocDbContext> options) : base(options) { }

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