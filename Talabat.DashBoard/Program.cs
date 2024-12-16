using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Talabat.Core.Entities.Identity;
using Talabat.Repositry.Data;
using Talabat.Repositry.IdentityDB;

namespace Talabat.DashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StoreDbcontext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DufltConnection")));

            builder.Services.AddDbContext<AppIdentityDbContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequireNonAlphanumeric=true;
                //options.Password.RequireDigit=true;
                //options.Password.RequireLowercase=true;

            })
                .AddEntityFrameworkStores<AppIdentityDbContext>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}