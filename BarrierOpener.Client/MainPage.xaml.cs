using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;

namespace BarrierOpener.Client;

public partial class MainPage : ContentPage
{
    private readonly Guid _secret = new("1e7e5e8c-166b-411c-a16f-6bca38cf8dd6");
    private readonly string _resourceName = "action";

    private readonly IFirebaseRepository _firebaseRepository;

    public MainPage(IFirebaseRepository firebaseRepository)
    {
        _firebaseRepository = firebaseRepository;
        InitializeComponent();
    }

    private void OnOpenerClicked(object sender, EventArgs e)
    {
        OpenerBtn.DisableLayout = true;
        var message = new BarrierOpenMessage
        {
            RequestDateTime = DateTime.UtcNow,
            SecretKey = _secret,
            DeviceName =
                $"{DeviceInfo.Current.Model};{DeviceInfo.Current.Name};{DeviceInfo.Current.Idiom}{DeviceInfo.Current.VersionString}"
        };
        _firebaseRepository.SendMessage(_resourceName, message);
        OpenerBtn.DisableLayout = false;
    }
}