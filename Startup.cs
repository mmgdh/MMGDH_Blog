using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMGDH_Blog.Data;
using MMGDH_Blog.Model;
using MMGDH_Blog.Services;
using System;

namespace MMGDH_Blog
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting=false);
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddDbContext<DataContext>(Option =>
            {
                Option.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));//_configuration["ConnectionStrings:DefaultConnection"]
            });
            services.AddDbContext<IdentityDbContext>(Options =>
                    Options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MMGDH_Blog")));

            services.AddScoped<IRepository<Article>, ArticleService>();
            services.AddScoped<IRepository<Project>, ProjectService>();
            services.AddScoped<IRepository<CodeBase>, CodeBaseService>();
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IConfiguration configuration
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseMvc(buider =>
            {
                buider.MapRoute("Default", "{controller=Index}/{action=Index}/{id?}");
            });
                       
        }
    }
}
