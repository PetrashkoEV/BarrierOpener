using System.Collections.ObjectModel;
using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;

namespace BarrierOpener.Server;

public partial class MainPage : ContentPage
{
    private readonly IFirebaseRepository _firebaseRepository;
    private readonly IFirebaseConfiguration _configuration;

    public ObservableCollection<BarrierOpenMessage> Actions { get; set; } = new();

    public MainPage(
        IBackgroundService service,
        IPhoneDialerService phoneService,
        IFirebaseRepository firebaseRepository,
        IFirebaseConfiguration configuration)
    {
        InitializeComponent();

        BindingContext = this;

        //service.Start(firebaseRepository, configuration, phoneService);
        service.Start();
    }

    private void OnSendBtnClicked(object sender, EventArgs e)
    {
        var message = new BarrierOpenMessage
        {
            RequestDateTime = DateTime.UtcNow,
            SecretKey = _configuration.SecretKey,
            DeviceName =
                $"{DeviceInfo.Current.Model};{DeviceInfo.Current.Name};{DeviceInfo.Current.Idiom}{DeviceInfo.Current.VersionString}"
        };
        _firebaseRepository.SendMessage(_configuration.ResourceName, message);
    }
}