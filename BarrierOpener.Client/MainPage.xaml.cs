using BarrierOpener.Client.ViewModels;
using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;

namespace BarrierOpener.Client;

public partial class MainPage : ContentPage
{
    private readonly IFirebaseRepository _firebaseRepository;
    private readonly IFirebaseConfiguration _configuration;
    private readonly MainPageViewModel _viewModel;

    public MainPage(IFirebaseRepository firebaseRepository, IFirebaseConfiguration configuration)
    {
        _firebaseRepository = firebaseRepository;
        _configuration = configuration;
        InitializeComponent();

        _viewModel = (MainPageViewModel)BindingContext;
    }

    private void OnOpenerClicked(object sender, EventArgs e)
    {
        _viewModel.ButtonBrush = Brush.Gray;
        _viewModel.ButtonEnabled = false;

        Task.Run(SendMessageToOpenBarrier);
    }

    private void SendMessageToOpenBarrier()
    {
        var message = new BarrierOpenMessage
        {
            RequestDateTime = DateTime.UtcNow,
            DeviceName =
                $"{DeviceInfo.Current.Model};{DeviceInfo.Current.Name};{DeviceInfo.Current.Idiom}{DeviceInfo.Current.VersionString}"
        };
        _firebaseRepository.SendMessage(_configuration.ResourceName, message);
        Thread.Sleep(TimeSpan.FromSeconds(10));

        _viewModel.ButtonBrush = Brush.Green;
        _viewModel.ButtonEnabled = true;
    }
}