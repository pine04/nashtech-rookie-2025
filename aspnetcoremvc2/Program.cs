using aspnetcoremvc2.Models;
using aspnetcoremvc2.Repositories;
using aspnetcoremvc2.Services;

namespace aspnetcoremvc2;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<IRookieRepository, InMemoryRookieRepository>();
        builder.Services.AddSingleton<IRookieService, RookieService>();
        builder.Services.AddSingleton<IExcelExportService<Person>, RookieExcelExportService>();

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
            name: "rookies",
            pattern: "NashTech/Rookie/{action=Index}/{id?}",
            defaults: new { controller = "Rookie" });

        app.MapControllerRoute(
            name: "default",
            pattern: "Home/{action=Index}/{id?}",
            defaults: new { controller = "Home" });

        app.Run();
    }
}
