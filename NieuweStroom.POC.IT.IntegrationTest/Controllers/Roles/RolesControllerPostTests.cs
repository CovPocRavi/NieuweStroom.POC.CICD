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
using FluentValidation.Results;
using Xunit;
using Xunit.Abstractions;

namespace NieuweStroom.POC.IT.IntegrationTest.Controllers.Roles
{
    public class RolesControllerPostTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        private string Description;
        private string Token;
        public RolesControllerPostTests(ITestOutputHelper output, Request<Startup> request, DbContextFactory contextFactory)
        {
            this.output = output;
            this.request = request;
            this.nieuweStroomPocDbContext = contextFactory.nieuweStroomPocDbContext;

            Description = "Valid Role";

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
            request.AddAuth(Token).Post("/api/roles", new { Description = Description });

        [Fact]
        public async Task ShouldSave_Category_IfInputValid()
        {
            await Exec();
            var roleInDb = nieuweStroomPocDbContext.Roles.FirstOrDefault(c => c.Description == Description);
            roleInDb.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldRetuns_Role_IfInputValid()
        {
            var res = await Exec();
            var body = await res.BodyAs<KeyValuePairResource>();

            body.Id.Should().BeGreaterThan(0, "Id should by set by EF");
            body.Description.Should().Be(Description, "Is the same description sended on Exec();");
        }

        [Fact]
        public async Task ShouldRetun_BadRequest400_IfDescription_LessThanFourCharacters()
        {
            Description = "a";

            output.WriteLine(Token);

            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Errors.Should().ContainKey("Description");
        }

        [Fact]
        public async Task ShouldRetun_BadRequest400_IfDescription_MoreThanThirtyTwoCharacters()
        {
            Description = string.Join("a", new char[34]);

            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Errors.Should().ContainKey("Description");
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