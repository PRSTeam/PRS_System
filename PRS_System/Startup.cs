using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_System.IServices;
using PRS_System.Services;

namespace PRS_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("ConnectionString");
        }

        public IConfiguration Configuration { get; }
        public static string ConnectionString { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IAccountService, AccountService>();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(8);
            });
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IInformationService, InformationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                //pattern: "{controller=FormPRS}/{action=form}/{id?}");
                //pattern: "{controller=AdminSetting}/{action=addnewuser}/{id?}");
                //    pattern: "{controller=FormPRS}/{action=Index}/{id?}");
                pattern: "{controller=FormPRS}/{action=index}/{id?}");
            });
        }
    }
}
