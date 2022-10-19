using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Spotify.Core;

namespace Spotify.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true, reloadOnChange: true);

#if DEBUG
        builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();
#else
        builder.Services.AddControllersWithViews();
#endif

        builder.Services.AddSingleton(new SpotifyClient(
            clientId: builder.Configuration.GetValue<string>("Spotify:ClientId"),
            clientSecret: builder.Configuration.GetValue<string>("Spotify:ClientSecret"),
            redirectUri: builder.Configuration.GetValue<string>("Spotify:RedirectUri")));

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = builder.Configuration["Auth:Cookie:Name"];

                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/Denied";

                options.Cookie.MaxAge = TimeSpan.FromHours(1);

                //options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

        builder.Services.AddAuthorization();

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(new AuthorizeFilter());
        });

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}