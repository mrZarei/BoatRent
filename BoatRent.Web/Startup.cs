using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BoatRent.Data;
using Microsoft.EntityFrameworkCore;
using BoatRent.Core.Interfaces;
using BoatRent.Core.Services;

namespace BoatRent.Web
{
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
            services.AddControllersWithViews();
            services.AddDbContext<RentDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("default"));
            });
            services.AddTransient<IBoatRentalRepository, RentRepository>();

            services.AddTransient(services =>
            {
                var hourlyFee = Configuration.GetValue<decimal>("PriceModel:HourlyFee");
                var basicFee = Configuration.GetValue<decimal>("PriceModel:BasicFee");
                var repo = services.GetService<IBoatRentalRepository>();
                return new RentService(repo,hourlyFee,basicFee);
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
