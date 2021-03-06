using System;
using System.Linq;
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
    public class CategoriesControllerDeleteTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;
        private int Id;

        private string Token;

        public CategoriesControllerDeleteTests(Request<Startup> request, DbContextFactory contextFactory, ITestOutputHelper output)
        {
            this.request = request;
            this.nieuweStroomPocDbContext = contextFactory.nieuweStroomPocDbContext;
            this.output = output;

            var category = new Category { Description = "Description" };

            nieuweStroomPocDbContext.Add(category);
            nieuweStroomPocDbContext.SaveChanges();

            Id = category.Id;

            nieuweStroomPocDbContext = contextFactory.GetRefreshContext();

            var user = new User()
            {
                Email = "",
                Name = "",
                Lastname = "",
                Id = 1
            };

            Token = request.Jwt.GenerateToken(user);
        }

        public void Dispose()
        {
            nieuweStroomPocDbContext.Categories.RemoveRange(nieuweStroomPocDbContext.Categories);
            nieuweStroomPocDbContext.SaveChanges();
        }

        public Task<HttpResponseMessage> Exec() =>
            request.AddAuth(Token).Delete($"/api/categories/{Id}");


        [Fact]
        public async Task ShouldReturns_404NotFound_IfInvalidId()
        {

            Id = -1;

            var result = await Exec();

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldDelete_TheCategory_IfValidId()
        {
            await Exec();

            var categoryInDb = nieuweStroomPocDbContext.Categories.FirstOrDefault(c => c.Id == Id);

            categoryInDb.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturn_401NotFound_IfNoTokenProvided()
        {
            Token = "";

            var res = await Exec();

            res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


    }
}