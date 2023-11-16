using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvCBornToMove.Data;
using MvCBornToMove.Models;

namespace MvCBornToMove
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MvCBornToMoveContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MvCBornToMoveContext") ?? throw new InvalidOperationException("Connection string 'MvCBornToMoveContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

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

            app.UseRequestLocalization("en-UY", "fr-FR");

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}