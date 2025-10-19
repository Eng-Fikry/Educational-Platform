using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Educational_platform.CustomeMiddelwares;
using Educational_platform.Extentions;
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

            #region System Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            #endregion

            #region Add Services

            builder.Services.AddInfrastructureServices(builder.Configuration);


            builder.Services.AddApplicationService();


            builder.Services.AddIdentityRegistration(builder.Configuration);

            #endregion

            var app = builder.Build();
            await app.DataSeed();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.AddMiddleWares();


            app.MapControllers();

            app.Run();
        }

        
    }
}