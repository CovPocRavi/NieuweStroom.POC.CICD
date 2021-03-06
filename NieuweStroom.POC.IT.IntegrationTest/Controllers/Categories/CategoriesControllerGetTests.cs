using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NieuweStroom.POC.IT.Controllers.Resources;
using NieuweStroom.POC.IT.Core.Entities;
using NieuweStroom.POC.IT.IntegrationTest.Extensions;
using NieuweStroom.POC.IT.IntegrationTest.Helpers;
using NieuweStroom.POC.IT.Persistance;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace NieuweStroom.POC.IT.IntegrationTest.Controllers.Categories
{
    public class CategoriesControllerGetTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;
        public CategoriesControllerGetTests(ITestOutputHelper output, Request<Startup> request, DbContextFactory contextFactory)
        {
            this.output = output;
            this.request = request;
            this.nieuweStroomPocDbContext = contextFactory.nieuweStroomPocDbContext;
        }
        public void Dispose()
        {
            nieuweStroomPocDbContext.Categories.RemoveRange(nieuweStroomPocDbContext.Categories);
            nieuweStroomPocDbContext.SaveChanges();
        }

        [Fact]
        public async Task ShouldReturn_AllCategories()
        {
            await nieuweStroomPocDbContext.Categories.AddRangeAsync(new Category[]{
                new Category(){ Description = "Category1" },
                new Category(){ Description = "Category2" }
            });

            await nieuweStroomPocDbContext.SaveChangesAsync();

            var response = await request.Get("/api/categories");
            var body = await response.BodyAs<KeyValuePairResource[]>();


            response.StatusCode.Should().Be(HttpStatusCode.OK);
            body.Length.Should().Be(2);
            body.Should().ContainSingle(gn => gn.Description == "Category1");
            body.Should().ContainSingle(gn => gn.Description == "Category2");
        }


    }
}