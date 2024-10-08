﻿using Amazon.S3;
using ChuckItApi.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ChuckItApi.Models;
using ChuckItApi.Services;
using dotenv.net;
using System;
using ChuckItApi.Services.Interfaces;
using ChuckItApi.Data.Seeds;

namespace ChuckItApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Load the .env file
            DotEnv.Load();

            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_DATABASE");
            var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var jwtKey = Environment.GetEnvironmentVariable("JwtKey");

            if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) ||
                string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword) ||
                string.IsNullOrEmpty(dbPort))
            {
                throw new ArgumentNullException("Database environment variables are not set correctly.");
            }

            var connectionString = $"Host={dbHost}; Port={dbPort}; Userid={dbUser}; Password={dbPassword}; Database={dbName}";

            // Use the connection string in DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging());


            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddDefaultTokenProviders();

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentException("JWT variables are not set correctly");
            }


            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "ChuckItAPI",
                    Version = "v1"
                });
            });

            services.AddAuthorization();

            // AWS Services
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddSingleton<IS3Service, S3Service>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IListingService, ListingService>();

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChuckItAPI V1");
                    //c.RoutePrefix = string.Empty; //Set Swagger UI at the app's root
                });
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ApplyMigrations(serviceProvider);

            DataSeeder.Seed(serviceProvider);

        }

        private void ApplyMigrations(IServiceProvider serviceProvider)
        {
            using(var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
