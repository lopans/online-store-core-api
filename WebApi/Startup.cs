﻿using Base;
using Base.DAL;
using Base.Identity.Entities;
using Base.Identity.IndetityServer;
using Base.Services;
using Data;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public class Startup
    {
        private readonly Initializer dataInitializer;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            dataInitializer = new Initializer();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"data source=(local);initial catalog=OnlineStore-core;integrated security=False;User ID=sa;Password=111111;";
            services.AddDbContext<DataContext>(op => op.UseSqlServer(connection));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerValidator>();
            services.AddTransient<ICorsPolicyService, ICorsPolicyProvider>();
            services.AddTransient<ICheckAccessService, CheckAccessService>();
            services.AddTransient<ISystemUnitOfWork, SystemUnitOfWork>();

            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<DataContext>();

            services.Configure<IdentityOptions>(opt => {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddMvcCore()
                .AddCors(opts => opts.AddPolicy("any", opt => opt
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    //.AllowCredentials()
                    .WithHeaders("*")
                    .AllowAnyOrigin()))
                .AddAuthorization()
                .AddJsonFormatters();
            services.AddAuthentication("Bearer")
              .AddCookie("dummy")
              .AddJwtBearer("Bearer", options =>
              {
                  options.Authority = "http://localhost:8200";
                  options.RequireHttpsMetadata = false;

                  options.Audience = "api1";
              });
            var builder = services.AddIdentityServer(options =>
            {
                options.Authentication.CookieAuthenticationScheme = "dummy";
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddCorsPolicyService<ICorsPolicyProvider>()
                .AddResourceOwnerValidator<ResourceOwnerValidator>()
                .AddAspNetIdentity<User>();

            builder.AddDeveloperSigningCredential();
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseMvc();

            dataInitializer.Seed(app).GetAwaiter().GetResult();
        }
    }
}
