namespace BillsManagement.API
{
    using AutoMapper;
    using BillsManagement.API.Configuration;
    using BillsManagement.Custom.CustomExceptions;
    using BillsManagement.Data.Models;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Security;
    using BillsManagement.Utility;
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
    using Microsoft.OpenApi.Models;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Text;
    using BillsManagement.Data.Repositories;
    using BillsManagement.Data;
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Business.Services.AuthService;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Business.Services.ChargesService;
    using BillsManagement.Business.Services.OccupantsService;

    /// <summary>
    /// The application startup class
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _assemblyName;
        private readonly string _assemblyVersion;
        private readonly string _applicationBaseDirectory;

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            _assemblyName = assemblyName.Name;
            _assemblyVersion = $"v{assemblyName.Version?.Major ?? 1}";
            _applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Users secrets
            services.Configure<Secrets>(this._configuration.GetSection("Secrets"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_assemblyVersion, new OpenApiInfo { Title = _assemblyName, Version = _assemblyVersion });
                c.IncludeXmlComments($"{_applicationBaseDirectory}\\{ApiConstants.CoreXmlDocumentation}");
                c.IncludeXmlComments($"{_applicationBaseDirectory}\\{ApiConstants.DomainModelsXmlDocumentation}");
            });

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddMvc()
                .AddMvcOptions(mvc => mvc.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            // DbContext configuration
            //var connectionString = this._configuration["Secrets:JWT_Secret"];
            services.AddDbContext<BillsManager_DevContext>(options =>
                options.UseSqlServer(this._configuration.GetConnectionString("BillsManagerConnectionString")));

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

            var key = Encoding.UTF8.GetBytes(this._configuration["Secrets:JWT_Secret"]);

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

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{this._assemblyVersion}/swagger.json", this._assemblyName);
            });

            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseMvc();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
