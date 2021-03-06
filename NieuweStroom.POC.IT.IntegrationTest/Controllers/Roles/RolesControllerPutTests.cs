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

namespace NieuweStroom.POC.IT.IntegrationTest.Controllers.Roles
{
    public class RolesControllerPutTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        private int Id;
        private string Description;

        private string Token;
        public RolesControllerPutTests(ITestOutputHelper output, Request<Startup> request, DbContextFactory contextFactory)
        {
            this.output = output;
            this.request = request;
            this.nieuweStroomPocDbContext = contextFactory.nieuweStroomPocDbContext;

            Description = "New Description";

            var role = new Role { Description = "Description" };

            nieuweStroomPocDbContext.Roles.Add(role);
            nieuweStroomPocDbContext.SaveChanges();

            Id = role.Id;

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
            nieuweStroomPocDbContext.Roles.RemoveRange(nieuweStroomPocDbContext.Roles);
            nieuweStroomPocDbContext.SaveChanges();
        }

        public Task<HttpResponseMessage> Exec() =>
            request.AddAuth(Token).Put($"/api/roles/{Id}", new { Description = Description });


        [Fact]
        public async Task ShouldUpdate_ExistingRole_IfExistAndValid()
        {
            await Exec();

            var roleInDb = nieuweStroomPocDbContext.Roles.FirstOrDefault(c => c.Id == Id);

            roleInDb.Description.Should().Be("New Description");
        }


        [Fact]
        public async Task ShouldReturn_UpdatedRole_IfExistAndValid()
        {
            var res = await Exec();
            var body = await res.BodyAs<KeyValuePairResource>();

            body.Description.Should().Be("New Description");
            body.Id.Should().Be(Id);
        }

        [Fact]
        public async Task ShouldReturn_400BadRequest_IdDescriptionLessThanFourCharacters()
        {
            Description = "a";

            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Errors.Should().ContainKey("Description");
        }

        [Fact]
        public async Task ShouldReturn_400BadRequest_IdDescriptionMoreThanSixtyFourCharacters()
        {
            Description = string.Join("a", new char[34]);

            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Errors.Should().ContainKey("Description");
        }

        [Fact]
        public async Task ShouldReturn_404NotFound_IfIdIsInvalid()
        {
            Id = -1;

            var res = await Exec();

            res.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task ShouldRetun_Unauthorized401_IfNoTokenProvided()
        {
            Token = "";

            var res = await Exec();

            res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }


    }
}