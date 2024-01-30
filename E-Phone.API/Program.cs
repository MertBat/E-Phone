using E_Phone.BLL;
using E_Phone.BLL.AutoMapper;
using E_Phone.BLL.Handlers.Abstract;
using E_Phone.Core.Entities;
using E_Phone.Core.IRepositories.BaseRepository;
using E_Phone.DAL;
using E_Phone.DAL.Context;
using E_Phone.DAL.Repositories.BaseRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace E_Phone.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Context
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnectionString")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Jwt Ayarlarý
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("User", options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"])),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
                };
            });

            //Swagger Authorization
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Phone", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "JWT'nin baþýnda 'Bearer ' olacak þekilde yazýnýz.",
                    In = ParameterLocation.Header,
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

            //Repository Injections
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //Service Injections
            ServiceRegistrations.ServiceInjections(builder.Services);

            //Validations
            ValidationRegistrations.ValidationControllers(builder.Services);

            //AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //Özel Endpointler
            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "GetAllModels",
                     pattern: "brands/{brandId}/models",
                     defaults: new { controller = "Model", action = "GetAllModels" }
                );

                 endpoints.MapControllerRoute(
                     name: "CreateModel",
                     pattern: "brands/{brandId}/models",
                     defaults: new { controller = "Model", action = "CreateModel" }
                 );

                 endpoints.MapControllerRoute(
                     name: "GetAllVersions",
                     pattern: "models/{modelId}/versions",
                     defaults: new { controller = "Version", action = "GetAllVersions" }
                     );

                 endpoints.MapControllerRoute(
                     name: "CreateVersion",
                     pattern: "models/{modelId}/versions",
                     defaults: new { Controllers = "Model", action = "CreateVersion" }
                     );

                 endpoints.MapControllerRoute(
                     name: "GetOrders",
                     pattern: "customers/{customerId}/orders",
                     defaults: new { Controllers = "Order", action = "GetCustomerOrders" }
                     );
             });

            app.Run();
        }
    }
}