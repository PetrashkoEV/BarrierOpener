using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using System.Reactive;

namespace BarrierOpener.Server.Services;

[Service]
public partial class BackgroundService : Service
{
    partial void StartInternal(string url)
    {
        var dataUrl = Android.Net.Uri.Parse(url);
        var startService = new Intent(MainActivity.ActivityCurrent, typeof(BackgroundService));
        startService.SetAction("START_SERVICE");
        startService.SetData(dataUrl);
        MainActivity.ActivityCurrent.StartService(startService);
    }

    partial void StopInternal()
    {
        Intent stopIntent = new Intent(MainActivity.ActivityCurrent, this.Class);
        stopIntent.SetAction("STOP_SERVICE");
        MainActivity.ActivityCurrent.StartService(stopIntent);
    }

    public override IBinder? OnBind(Intent? intent)
    {
        throw new NotImplementedException();
    }

    [return: GeneratedEnum]//we catch the actions intents to know the state of the foreground service
    public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        if (intent.Action == "START_SERVICE")
        {
            ExecuteProcess(intent.DataString);
        }
        else if (intent.Action == "STOP_SERVICE")
        {
            StopProcess();
            StopForeground(true);//Stop the service
            StopSelfResult(startId);
        }

        return StartCommandResult.NotSticky;
    }

    //private void RegisterNotification()
    //{
    //    NotificationChannel channel = new NotificationChannel("ServiceChannel", "ServiceDemo", NotificationImportance.Max);
    //    NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
    //    manager.CreateNotificationChannel(channel);
    //    Notification notification = new Notification.Builder(this, "ServiceChannel")
    //        .SetContentTitle("Service Working")
    //        .SetSmallIcon(Resource.Drawable.abc_ab_share_pack_mtrl_alpha)
    //        .SetOngoing(true)
    //        .Build();

    //    StartForeground(100, notification);
    //}

    //public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
    //{
    //    // Code not directly related to publishing the notification has been omitted for clarity.
    //    // Normally, this method would hold the code to be run when the service is started.

    //    var notification = new Notification.Builder(this)
    //        .SetContentTitle(Resources.GetString(Resource.String.app_name))
    //        .SetContentText(Resources.GetString(Resource.String.notification_text))
    //        .SetSmallIcon(Resource.Drawable.ic_stat_name)
    //        .SetContentIntent(BuildIntentToShowMainActivity())
    //        .SetOngoing(true)
    //        .AddAction(BuildRestartTimerAction())
    //        .AddAction(BuildStopServiceAction())
    //        .Build();

    //    // Enlist this instance of the service as a foreground service
    //    StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);
    //}
}