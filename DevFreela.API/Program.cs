using DevFreela.API.Extensions;
using DevFreela.API.Filters;
using DevFreela.API.Models;
using DevFreela.Application.Commands.Projects;
using DevFreela.Application.Consumers;
using DevFreela.Application.Validators;
using DevFreela.Infrastructure.Persistence;
using EntityFrameworkCore.UseRowNumberForPaging;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace DevFreela
{
    public class Program
    {       
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            ConfigureServices(builder);

            // Configure the HTTP request pipeline
            ConfigureApplication(builder);
        }

        private static void ConfigureApplication(WebApplicationBuilder builder)
        {
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

            builder.Services.AddHttpClient();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header usando o esquema Bearer."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                 });
            });

            builder.Services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidAudience = builder.Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                   };
               });

            // DbContext
            var connectionString = builder.Configuration.GetConnectionString("DevFreelaConnStr");

            builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString, o => o.UseRowNumberForPaging()));

            // Mediatr
            builder.Services.AddMediatR(typeof(CreateProjectCommand)); // Info: you just need to informe one class from the Assembly, and all the classes from the whole assembly will be gathered to use

            // Configurations
            builder.Services.Configure<OpeningTimeOption>(builder.Configuration.GetSection("OpeningTime"));
            builder.Services.AddInfrastructure();
            builder.Services.AddHostedService<PaymentApprovedConsumer>();

            // Fluent Validation
            builder.Services.AddFluentValidation(q => q.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

            #region In memory

            // Install: Microsoft.EntityFrameworkCore.InMemory 
            // builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseInMemoryDatabase("DevFreela"));

            #endregion

        }
    }
}