using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityProvider;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using static Duende.IdentityServer.IdentityServerConstants;

namespace Identity.Controllers
{
    [Authorize(Policy = LocalApi.PolicyName, Roles = "admin")]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserManager<ExtendedIdentityUser> userManager;
        public UserController(UserManager<ExtendedIdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpGet]
        [Route("/users")]
        public List<UserDto> Get()
        {
            List<UserDto> userIds = userManager.Users.Select(user => new UserDto(user.Id, user.UserName)).ToList();
            return userIds;
        }


    }
}
