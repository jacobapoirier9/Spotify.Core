using Spotify.Core;

namespace Spotify.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("appsettings.Secret.json", optional: true, reloadOnChange: true);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddSingleton(new SpotifyTokenClient(
            clientId: builder.Configuration.GetValue<string>("Spotify.ClientId"),
            clientSecret: builder.Configuration.GetValue<string>("Spotify.ClientSecret"),
            redirectUri: builder.Configuration.GetValue<string>("Spotify.RedirectUri")));

        builder.Services.AddSingleton(new SpotifyClient());

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