using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using BarrierOpener.Domain.Configuration;
using BarrierOpener.Domain.Factory;
using Firebase.Auth;
using Firebase.Auth.Providers;

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

        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
        {
            ApiKey = "AIzaSyA8K_ktLrpLbL73o0MO0whm8m8AEBt-pjU",
            AuthDomain = "barrieropener.firebaseapp.com",
            Providers =
            [
                new EmailProvider()
            ]
        }));

        builder.Services.AddSingleton<IFirebaseRepository, FirebaseRepository>();
        builder.Services.AddSingleton<IFirebaseConfiguration, FirebaseConfiguration>();
        builder.Services.AddSingleton<IFirebaseClientFactory, FirebaseClientFactory>();
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