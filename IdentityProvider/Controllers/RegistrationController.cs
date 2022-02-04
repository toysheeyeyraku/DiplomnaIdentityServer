using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
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
    public class RegistrationController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        public RegistrationController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<string> Post([FromBody] RegisterUserRequest registerUserRequest)
        {
            bool isUserExist = await (IsUserExist(registerUserRequest));
            if (!isUserExist)
            {
                await CreateUser(registerUserRequest);
            }
            return "success";
        }

        private async Task<bool> IsUserExist(RegisterUserRequest registerUserRequest)
        {

            IdentityUser user = await userManager.FindByNameAsync(registerUserRequest.UserName);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task CreateUser(RegisterUserRequest registerUserRequest)
        {
            var identityUser = new IdentityUser(registerUserRequest.UserName)
            {
                //Id = Guid.NewGuid().ToString()
        };
            var claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Role, registerUserRequest.Role)
                    };

            var result = await userManager.CreateAsync(identityUser, registerUserRequest.Password);
            result = await userManager.AddClaimsAsync(identityUser, claims);
        }
    }
}
