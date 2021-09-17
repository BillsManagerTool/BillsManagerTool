﻿namespace BillsManagement.Core
{
    using AutoMapper;
    using BillsManagement.Core.Configuration;
    using BillsManagement.Core.CustomExceptions;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository;
    using BillsManagement.Repository.Repositories;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Services.Services.AuthService;
    using BillsManagement.Services.Services.ChargesService;
    using BillsManagement.Services.Services.OccupantsService;
    using BillsManagement.Utility.Security;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Users secrets
            services.Configure<Secrets>(Configuration.GetSection("Secrets"));

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Inject AppSetting
            //services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services
                .AddMvc()
                .AddMvcOptions(mvc => mvc.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            // DbContext configuration
            var connectionString = Configuration["Secrets:JWT_Secret"];
            services.AddDbContext<BillsManager_DevContext>(options =>
                options.UseSqlServer(Configuration["Secrets:ConnectionString"]));

            // Repository configurations
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IChargesRepository, ChargesRepository>();
            services.AddScoped<IOccupantRepository, OccupantRepository>();

            services.AddScoped<IJwtUtils, JwtUtils>();

            // Service configurations
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IChargesService, ChargesService>();
            services.AddScoped<IOccupantService, OccupantsService>();

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // JWT Authentication

            var key = Encoding.UTF8.GetBytes(Configuration["Secrets:JWT_Secret"]);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Policy based authorization
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy("Housekeeper", policyBuilder =>
                {
                    policyBuilder.AddRequirements(new HousekeeperRequirement(true));
                });
            });

            services.AddSingleton<IAuthorizationHandler, HousekeeperRequirementHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<CustomExceptionMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
