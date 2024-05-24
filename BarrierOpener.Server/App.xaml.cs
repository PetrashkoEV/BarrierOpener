using BarrierOpener.Server.Core;

namespace BarrierOpener.Server;

public partial class App : Application
{
    public App(IFirebaseListenerService listener)
    {
        InitializeComponent();
        
        listener.RegisterListener(DateTime.UtcNow);

        MainPage = new AppShell();
    }
}