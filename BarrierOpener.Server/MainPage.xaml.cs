using BarrierOpener.Server.DataBase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using BarrierOpener.Server.Services;
using Firebase.Database.Streaming;

namespace BarrierOpener.Server;

public partial class MainPage : ContentPage
{
    private FirebaseClient _firebaseClient;
    private string _resourceName = "action";

    public ObservableCollection<BarrierActionMessage> Actions { get; set; } = new();

    public MainPage()
    {
        _firebaseClient = new FirebaseClient(baseUrl: "https://barrieropener-default-rtdb.firebaseio.com/");

        InitializeComponent();

        BindingContext = this;

        var collection = _firebaseClient
            .Child(_resourceName)
            .AsObservable<BarrierActionMessage>()
            .Subscribe(Listener);
    }

    private void Listener(FirebaseEvent<BarrierActionMessage> item)
    {
        if (item.Object != null)
        {
            Actions.Add(new BarrierActionMessage
            {
                Message = item.Object.Message + PhoneDialer.Default.IsSupported,
            });

            if (PhoneDialer.Default.IsSupported)
            {
                var dialer = new PhoneDialerService();
                dialer.CallPhone("+375445837994");
            }
            
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _firebaseClient.Child(_resourceName).PostAsync(new BarrierActionMessage
        {
            Message = TitleEntry.Text
        });

        TitleEntry.Text = string.Empty;
    }
}