using System;
using System.Net;
using System.Threading.Tasks;
using NieuweStroom.POC.IT.IntegrationTest.Extensions;
using FluentAssertions;
using NieuweStroom.POC.IT.Controllers.Resources;
using NieuweStroom.POC.IT.Core.Entities;
using NieuweStroom.POC.IT.IntegrationTest.Helpers;
using NieuweStroom.POC.IT.Persistance;
using Xunit;
using Xunit.Abstractions;

namespace NieuweStroom.POC.IT.IntegrationTest.Controllers.Roles
{
    public class RolesControllerGetByIdTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly CleanVidlyDbContext context;
        public RolesControllerGetByIdTests(ITestOutputHelper output, Request<Startup> request, DbContextFactory contextFactory)
        {
            this.output = output;
            this.request = request;
            this.context = contextFactory.Context;
        }

        public void Dispose()
        {
            context.Roles.RemoveRange(context.Roles);
            context.SaveChanges();
        }

        [Fact]
        public async Task ShouldReturn_Roles_ById()
        {
            var role = new Role() { Description = "Role 1" };
            await context.Roles.AddAsync(role);
            await context.SaveChangesAsync();

            var response = await request.Get($"/api/roles/{role.Id}");
            var body = await response.BodyAs<KeyValuePairResource>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            body.Description.Should().Be(role.Description);
        }

        [Fact]
        public async Task ShouldReturn_404NotFound_ById()
        {
            var response = await request.Get("/api/roles/1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}