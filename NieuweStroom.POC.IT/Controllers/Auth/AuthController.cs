using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NieuweStroom.POC.IT.Controllers.Users;
using NieuweStroom.POC.IT.Core.Abstract;
using NieuweStroom.POC.IT.Core.Entities;
using NieuweStroom.POC.IT.Extensions;
using NieuweStroom.POC.IT.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace NieuweStroom.POC.IT.Controllers.Auth
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly JwtAuthentication jwtAuthentication;

        public AuthController(JwtAuthentication jwtAuthentication, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.jwtAuthentication = jwtAuthentication; //gfdgfdg blbblblblblbl
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthResource authResource)
        {
            var user = await userRepository.GetWithRoles(u => u.Email == authResource.Email);
            if (user is null) return this.BadRequest("Authorization", "Invalid email or password");

            var validPassword = Hashing.VerifyHash(authResource.Password, user.Salt, user.Password);
            if (!validPassword) return this.BadRequest("Authorization", "Invalid email or password");

            var token = jwtAuthentication.GenerateToken(user);
            return Ok(new { token });
        }


    }
}