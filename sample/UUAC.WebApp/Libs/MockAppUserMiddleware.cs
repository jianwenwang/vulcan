﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UUAC.Common;
using Vulcan.AspNetCoreMvc.Interfaces;

namespace UUAC.WebApp.Libs
{
  
    public class MockAppUserMiddleware
    {
        private readonly RequestDelegate _next;
      
        private readonly MockAppUserOption _option;
    
        public MockAppUserMiddleware(RequestDelegate next,  MockAppUserOption option = null)
        {
            _next = next;
            _option = option ?? new MockAppUserOption() { MockUserId = "admin" };
            
        }

        public async Task Invoke(HttpContext context)
        {

            if (!context.User.Identity.IsAuthenticated)
            {
                string userId = _option.MockUserId;
             
                const string Issuer = "https://vulcan.com";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userId, ClaimValueTypes.String, Issuer),
                
                };


                var userIdentity = new ClaimsIdentity("SuperSecureLogin");
                userIdentity.AddClaims(claims);
                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await context.Authentication.SignInAsync(Constans.AuthenticationScheme, userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
            }

          
            await _next.Invoke(context);
          
        }
    }
    public class MockAppUserOption
    {
        public string MockUserId { get; set; }
    }
    public static class MockAppUserExtensions
    {
        public static IApplicationBuilder UseMockAppUser(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MockAppUserMiddleware>();
        }
        public static IApplicationBuilder UseMockAppUser(this IApplicationBuilder builder, MockAppUserOption option)
        {
            return builder.UseMiddleware<MockAppUserMiddleware>(option);
        }
    }
}
