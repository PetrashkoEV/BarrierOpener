using System.Collections.ObjectModel;
using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;
using BarrierOpener.Server.Core;

namespace BarrierOpener.Server;

public partial class MainPage : ContentPage
{
    private readonly DateTime _applicationStartTime;

    private readonly IPhoneDialerService _phoneService;
    private readonly IFirebaseRepository _firebaseRepository;
    private readonly IFirebaseConfiguration _configuration;


    public ObservableCollection<BarrierOpenMessage> Actions { get; set; } = new();

    public MainPage(IPhoneDialerService phoneService,
        IFirebaseRepository firebaseRepository,
        IFirebaseConfiguration configuration)
    {
        _phoneService = phoneService;
        _firebaseRepository = firebaseRepository;
        _configuration = configuration;

        _applicationStartTime = DateTime.Now;

        InitializeComponent();

        BindingContext = this;

        _firebaseRepository.RegisterObserver<BarrierOpenMessage>(_configuration.ResourceName, Listener);
    }
    
    private bool Listener(BarrierOpenMessage message)
    {
        if (message.SecretKey == _configuration.SecretKey && message.RequestDateTime > _applicationStartTime)
        {
            if (PhoneDialer.Default.IsSupported)
            {
                _phoneService.CallPhone(_configuration.PhoneNumber);
            }
        }

        return true;
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