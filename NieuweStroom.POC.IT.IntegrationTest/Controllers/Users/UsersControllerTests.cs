using System;
using NieuweStroom.POC.IT.Persistance;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Net.Http;
using NieuweStroom.POC.IT.Core.Entities;
using System.Linq;
using NieuweStroom.POC.IT.IntegrationTest.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Xunit.Abstractions;
using Newtonsoft.Json;
using NieuweStroom.POC.IT.Controllers.Resources;
using NieuweStroom.POC.IT.Controllers.Users;
using NieuweStroom.POC.IT.IntegrationTest.Helpers;

namespace NieuweStroom.POC.IT.IntegrationTest.Controllers.Users
{
    public class UsersControllerTests : IClassFixture<Request<Startup>>, IClassFixture<DbContextFactory>, IDisposable
    {
        private readonly Request<Startup> request;
        private readonly ITestOutputHelper output;
        private readonly NieuweStroomPocDbContext nieuweStroomPocDbContext;

        private readonly SaveUserResource userResource;

        public UsersControllerTests(ITestOutputHelper output, Request<Startup> request, DbContextFactory contextFactory)
        {
            this.output = output;
            this.request = request;
            this.nieuweStroomPocDbContext = contextFactory.nieuweStroomPocDbContext;

            var role = new Role { Description = "Description" };
            nieuweStroomPocDbContext.Roles.Add(role);
            nieuweStroomPocDbContext.SaveChanges();
            nieuweStroomPocDbContext = contextFactory.GetRefreshContext();

            userResource = new SaveUserResource
            {
                Email = "test@test.com",
                Name = "12345",
                Lastname = "1233456",
                Password = "1233456",
            };

            userResource.Roles.Add(role.Id);
        }

        public void Dispose()
        {
            nieuweStroomPocDbContext.Users.RemoveRange(nieuweStroomPocDbContext.Users);
            nieuweStroomPocDbContext.Roles.RemoveRange(nieuweStroomPocDbContext.Roles);
            nieuweStroomPocDbContext.SaveChanges();
        }

        public Task<HttpResponseMessage> Exec() => request.Post("/api/users", userResource);

        //[Fact]
        //public async Task ShouldAdd_NewUser_IfInputIsValid()
        //{
        //    await Exec();

        //   var userInDb = nieuweStroomPocDbContext.Users.FirstOrDefault(u => u.Name == userResource.Name && u.Lastname == userResource.Lastname && u.Email == userResource.Email); // test
            
        //    userInDb.Should().NotBeNull();
        //}

        [Fact]
        public async Task ShouldReturn_Header_WithToken()
        {
            var res = await Exec();
            res.Headers.Should().Contain(d => d.Key == "x-auth-token");
        }

        [Fact]
        public async Task ShouldReturns_TheNewUser_OnResponseBody()
        {
            var res = await Exec();
            var body = await res.BodyAs<UserResource>();

            body.Should().BeEquivalentTo(this.userResource, opt => opt
                .Excluding(x => x.Password) //Password is not in body response 
                .Excluding(x => x.Roles) //Exclude roles cause have diferent shape
            );
        }


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("InvalidFormat")]
        [InlineData("test@.com")]
        [InlineData("@cr.com")]
        [InlineData("test@cr.")]
        [InlineData("test.cr.com")]
        public async Task ShouldReturns_400BadRequest_EmailIsNotWithValidFormat(string email)
        {
            userResource.Email = email;
            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();
            body.Errors.Should().ContainKey("Email");
        }

        [Fact]
        public async Task ShouldReturns_400_IfEmailAlreadyRegistered()
        {
            await nieuweStroomPocDbContext.AddAsync(new User()
            {
                JoinDate = DateTime.Now,
                Name = userResource.Name,
                Lastname = userResource.Lastname,
                Email = userResource.Email,
                Password = new byte[64],
                Salt = new byte[128]
            });
            await nieuweStroomPocDbContext.SaveChangesAsync();

            var res = await Exec();
            var body = await res.BodyAs<ValidationErrorResource>();

            body.Errors.Should().ContainKey("Email");
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}