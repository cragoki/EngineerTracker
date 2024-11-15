using EngineerTracker.Clients;
using EngineerTracker.Services;
using EngineerTracker.Services.Interfaces;
using EngineerTracker.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EngineerTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Load configuration from appsettings.json, this is done slightly different to Blazor
            using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json").Result;
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            builder.Configuration.AddConfiguration(config);

            // Register as Scoped so the base Url is stored
            builder.Services.AddScoped(sp =>
            {
                var baseUrl = sp.GetRequiredService<IConfiguration>()["ApiSettings:BaseUrl"];
                return new HttpClient { BaseAddress = new Uri(baseUrl) };
            });

            builder.Services.AddScoped<ApiClient>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddScoped<MainPage>();
            builder.Services.AddScoped<MainViewModel>();

            builder.Services.AddTransient<EngineerAppointments>();
            builder.Services.AddTransient<EngineerAppointmentsViewModel>();

            builder.Services.AddTransient<ViewAppointment>();
            builder.Services.AddTransient<ViewAppointmentViewModel>();

            builder.Services.AddScoped<INavigationService, NavigationService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}