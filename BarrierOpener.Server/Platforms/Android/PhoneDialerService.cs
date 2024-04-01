using Android.Content;

namespace BarrierOpener.Server.Services;

public partial class PhoneDialerService
{
    partial void CallPhoneInternal(string number)
    {
        var callIntent = new Intent(Intent.ActionCall);
        callIntent.SetFlags(ActivityFlags.NewTask);

        var url = Android.Net.Uri.Parse($"tel:{number}");
        callIntent.SetData(url);
        Android.App.Application.Context.StartActivity(callIntent);
    }
}