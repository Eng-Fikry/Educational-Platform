using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Educational_platform.CustomeMiddelwares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Presistance;
using Presistance.DataSeeding;
using Presistance.Identity;
using Presistance.Reposatories;
using Service;
using Service.Mapping;
using Service_Abstraction;
using Shared.ErrorHandel;

namespace Educational_platform
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Add Services
            builder.Services.AddDbContext<PlatformDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("PlatformConnection"));
            });
            builder.Services.AddDbContext<PlatformIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IDataSeed,DataSeed>();
            builder.Services.AddScoped<IServiceManger, ServiceManger>();  
            
            builder.Services.AddAutoMapper(x => x.AddProfile( new IdentityProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile( new TeacherProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile( new StudentProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile( new CourseProfile()));

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
            .AddEntityFrameworkStores<PlatformIdentityDbContext>().AddDefaultTokenProviders();

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issure"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],

                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]!)),
                    
                };
                options.Events =  new JwtBearerEvents()
                {

                    OnChallenge = context =>
                    {
                        return WriteError(context.Response,StatusCodes.Status401Unauthorized,"You Are Not Authorized");

                    },
                    OnForbidden = context =>
                    {
                        return WriteError(context.Response, StatusCodes.Status403Forbidden, "You are not allowed to access this resource.");
                    },



                };
            });

            #endregion

            var app = builder.Build();
            var Scope = app.Services.CreateScope();
            var dataseeding = Scope.ServiceProvider.GetRequiredService<IDataSeed>();
            await dataseeding.IdentityDataSeed();





            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<CustomeExceptionHandler>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static Task WriteError(HttpResponse Response,int statuscode,string message)
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