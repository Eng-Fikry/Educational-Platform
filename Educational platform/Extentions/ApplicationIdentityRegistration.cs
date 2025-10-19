using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Presistance.Identity;
using Shared.ErrorHandel;

namespace Educational_platform.Extentions
{
    public static class ApplicationIdentityRegistration
    {
        public static IServiceCollection AddIdentityRegistration(this IServiceCollection Services,IConfiguration _configuration)
        {
            Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
            .AddEntityFrameworkStores<PlatformIdentityDbContext>().AddDefaultTokenProviders();

            Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:Issure"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:Audience"],

                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]!)),

                };
                options.Events = new JwtBearerEvents()
                {

                    OnChallenge = context =>
                    {
                        return WriteError(context.Response, StatusCodes.Status401Unauthorized, "You Are Not Authorized");

                    },
                    OnForbidden = context =>
                    {
                        return WriteError(context.Response, StatusCodes.Status403Forbidden, "You are not allowed to access this resource.");
                    },



                };
            });
            return Services;
        }
        private static Task WriteError(HttpResponse Response, int statuscode, string message)
        {
            Response.StatusCode = statuscode;
            return Response.WriteAsJsonAsync(new ErrorToReturn()
            {
                StatusCode = statuscode,
                ErrorMessage = message

            });
        }
    }
}
