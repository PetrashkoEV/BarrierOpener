using System.Collections.ObjectModel;
using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;
using BarrierOpener.Server.Core;

namespace BarrierOpener.Server;

public partial class MainPage : ContentPage
{
    private readonly IFirebaseRepository _firebaseRepository;
    private readonly IFirebaseConfiguration _configuration;

    public ObservableCollection<BarrierOpenMessage> Actions { get; set; } = new();

    public MainPage(IFirebaseListenerService listenerService,
        IFirebaseRepository firebaseRepository,
        IFirebaseConfiguration configuration)
    {
        _firebaseRepository = firebaseRepository;
        _configuration = configuration;

        InitializeComponent();

        BindingContext = this;

        listenerService.MessageNotification = MessageNotification;
    }

    private void MessageNotification(BarrierOpenMessage message)
    {
        Actions.Add(message);
    }


    private void OnSendBtnClicked(object sender, EventArgs e)
    {
        var message = new BarrierOpenMessage
        {
            RequestDateTime = DateTime.UtcNow,
            DeviceName =
                $"{DeviceInfo.Current.Model};{DeviceInfo.Current.Name};{DeviceInfo.Current.Idiom}{DeviceInfo.Current.VersionString}"
        };
        _firebaseRepository.SendMessage(_configuration.ResourceName, message);
    }
}