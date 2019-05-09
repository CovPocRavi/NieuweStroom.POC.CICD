using System;
using NieuweStroom.POC.IT.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace NieuweStroom.POC.IT.IntegrationTest.Helpers
{
    public class DbContextFactory : IDisposable
    {
        public NieuweStroomPocDbContext nieuweStroomPocDbContext { get; private set; }
        public DbContextFactory()
        {
            var dbBuilder = GetContextBuilderOptions<NieuweStroomPocDbContext>("vidly_db");

            nieuweStroomPocDbContext = new NieuweStroomPocDbContext(dbBuilder.Options);
            nieuweStroomPocDbContext.Database.Migrate();
        }

        public void Dispose()
        {
            nieuweStroomPocDbContext.Dispose();
        }

        public NieuweStroomPocDbContext GetRefreshContext()
        {
            var dbBuilder = GetContextBuilderOptions<NieuweStroomPocDbContext>("vidly_db");
            nieuweStroomPocDbContext = new NieuweStroomPocDbContext(dbBuilder.Options);

            return nieuweStroomPocDbContext;
        }

        private DbContextOptionsBuilder<NieuweStroomPocDbContext> GetContextBuilderOptions<T>(string connectionStringName)
        {
            var connectionString = ConfigurationSingleton.GetConfiguration().GetConnectionString(connectionStringName);
            var contextBuilder = new DbContextOptionsBuilder<NieuweStroomPocDbContext>();
            var servicesCollection = new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();

            contextBuilder.UseSqlServer(connectionString).UseInternalServiceProvider(servicesCollection);

            return contextBuilder;
        }
    }
}