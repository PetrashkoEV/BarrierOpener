using System.Reflection;
using BarrierOpener.Domain.Configuration;
using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.Factory;
using BarrierOpener.Domain.Repository;
using BarrierOpener.Server.Core;
using BarrierOpener.Server.Services;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BarrierOpener.Server;

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

        builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig()
        {
            ApiKey = "AIzaSyA8K_ktLrpLbL73o0MO0whm8m8AEBt-pjU",
            AuthDomain = "barrieropener.firebaseapp.com",
            Providers =
            [
                new EmailProvider()
            ]
        }));

        builder.Services.AddTransient<IPhoneDialerService, PhoneDialerService>();
        builder.Services.AddSingleton<IFirebaseListenerService, FirebaseListenerService>();
        builder.Services.AddTransient<IFirebaseRepository, FirebaseRepository>();
        builder.Services.AddTransient<IFirebaseConfiguration, FirebaseConfiguration>();
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
        using var stream = a.GetManifestResourceStream("BarrierOpener.Server.appsettings.json");

        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();


        builder.Configuration.AddConfiguration(config);

        return builder;
    }
}