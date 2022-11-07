using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using NLog;
using NLog.Extensions.Logging;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spotify.Core;
using Spotify.Web.Models;
using Spotify.Web.Services;

namespace Spotify.Web;

public class Program
{
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders()
            .AddNLog();

        _logger.Debug("Configuring services..");

        builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true, reloadOnChange: true);

#if DEBUG
        builder.Services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();
#else
        builder.Services.AddControllersWithViews();
#endif

        builder.Services.AddSingleton<IDbConnectionFactory>(
            new OrmLiteConnectionFactory(builder.Configuration.GetConnectionString(Connection.SpotifyEnhancer), SqlServerDialect.Provider));

        builder.Services.AddSingleton<IDataService, SqlServerDataService>();

        builder.Services.AddSingleton(new SpotifyClient(
            clientId: builder.Configuration.GetValue<string>("Spotify:ClientId"),
            clientSecret: builder.Configuration.GetValue<string>("Spotify:ClientSecret"),
            redirectUri: builder.Configuration.GetValue<string>("Spotify:RedirectUri"))
        {
            LogResponses = (request, response) =>
            {
                _logger.Trace("{Method} {StatusCode} {RequestUri}", request.Method, response.StatusCode, request?.RequestUri);

                //using (var stream = response.Content.ReadAsStream())
                //using (var reader = new StreamReader(stream))
                //{
                //    _logger.Trace(reader.ReadToEnd());
                //}
            }
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = builder.Configuration["Auth:Cookie:Name"];

                options.LoginPath = "/Auth/Login";
                options.AccessDeniedPath = "/Auth/Denied";

                options.Cookie.MaxAge = TimeSpan.FromHours(1);

                options.SlidingExpiration = false;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.Cookie.SameSite = SameSiteMode.Lax;
            });

        builder.Services.AddAuthorization();

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(new AuthorizeFilter());
        });

        var app = builder.Build();
        _logger.Debug("Configuring application.. {Environment}", app.Environment.EnvironmentName);

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

        _logger.Info("Start!");
        app.Run();
    }
}