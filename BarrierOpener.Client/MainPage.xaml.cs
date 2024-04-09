using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;

namespace BarrierOpener.Client;

public partial class MainPage : ContentPage
{
    private readonly IFirebaseRepository _firebaseRepository;
    private readonly IFirebaseConfiguration _configuration;

    public MainPage(IFirebaseRepository firebaseRepository, IFirebaseConfiguration configuration)
    {
        _firebaseRepository = firebaseRepository;
        _configuration = configuration;
        InitializeComponent();
    }

    private void OnOpenerClicked(object sender, EventArgs e)
    {
        OpenerBtn.DisableLayout = true;
        var message = new BarrierOpenMessage
        {
            RequestDateTime = DateTime.UtcNow,
            SecretKey = _configuration.SecretKey,
            DeviceName =
                $"{DeviceInfo.Current.Model};{DeviceInfo.Current.Name};{DeviceInfo.Current.Idiom}{DeviceInfo.Current.VersionString}"
        };
        _firebaseRepository.SendMessage(_configuration.ResourceName, message);
        OpenerBtn.DisableLayout = false;
    }
}