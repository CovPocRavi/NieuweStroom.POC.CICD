using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NieuweStroom.POC.IT.Controllers.Resources;
using NieuweStroom.POC.IT.Core.Entities;
using NieuweStroom.POC.IT.Infrastructure;
using NieuweStroom.POC.IT.IntegrationTest.Extensions;
using NieuweStroom.POC.IT.IntegrationTest.Helpers;
using NieuweStroom.POC.IT.Persistance;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace NieuweStroom.POC.IT.IntegrationTest.Controllers.Auth
{
    public class AuthControllerTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        private string Email { get; set; }
        private string Password { get; set; }
        private string Name { get; set; }
        private string Lastname { get; set; }

        public AuthControllerTests(Request<Startup> request, DbContextFactory contextFactory, ITestOutputHelper output)
        {
            this.request = request;
            this.nieuweStroomPocDbContext = contextFactory.nieuweStroomPocDbContext;
            this.output = output;

            CreateUserForTests();
        }

        private Task<HttpResponseMessage> Exec() => request.Post("/api/auth", new { Email, Password });


        [Fact]
        public async Task Authenticate_ShouldReturns400_IfEmailDoesntExist()
        {
            Email = "error@tset.com";
            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //First error message
            body.Errors[body.Errors.Keys.First()].First().Should().NotBeEmpty();
        }


        [Fact]
        public async Task Authenticate_ShouldReturns400_InvalidPassword()
        {
            Password = "invalidPassword";
            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //First error message
            body.Errors[body.Errors.Keys.First()].First().Should().NotBeEmpty();
        }

        [Fact]
        public async Task Authenticate_ShouldReturns200_IfValidInputPassed()
        {
            var res = await Exec();
            res.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Authenticate_ShouldReturnsToken_IfValidInputPassed()
        {
            var res = await Exec();
            var body = await res.BodyAs<TokenResource>();

            body.Token.Should().NotBeEmpty();
        }

        public void Dispose()
        {
            nieuweStroomPocDbContext.Roles.RemoveRange(nieuweStroomPocDbContext.Roles);
            nieuweStroomPocDbContext.SaveChanges();
        }

        private void CreateUserForTests()
        {
            Email = "test@test.com";
            Password = "abcd1234";
            Name = "1234567";
            Lastname = "6543210";

            var salt = Hashing.GenerateSalt();
            var hash = Hashing.CreateHash(salt, Password);

            var user = new User
            {
                Email = Email,
                JoinDate = DateTime.UtcNow,
                Name = Name,
                Lastname = Lastname,
                Salt = salt,
                Password = hash,
            };

            user.UserRoles.Add(new UserRole
            {
                Role = new Role
                {
                    Description = "Admin"
                }
            });

            nieuweStroomPocDbContext.Users.Add(user);
            nieuweStroomPocDbContext.SaveChanges();
        }
    }
}