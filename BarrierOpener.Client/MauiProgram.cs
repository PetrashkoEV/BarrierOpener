using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using BarrierOpener.Domain.Configuration;

namespace BarrierOpener.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .BuildConfiguration();

        builder.Services.AddSingleton<IFirebaseRepository, FirebaseRepository>();
        builder.Services.AddSingleton<IFirebaseConfiguration, FirebaseConfiguration>();
        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder BuildConfiguration(this MauiAppBuilder builder)
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("BarrierOpener.Client.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();


        builder.Configuration.AddConfiguration(config);

        return builder;
    }
}