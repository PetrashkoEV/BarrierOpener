using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;
using BarrierOpener.Domain.Repository;

namespace BarrierOpener.Server.Services;

public partial class BackgroundService : IBackgroundService
{
    protected IFirebaseRepository? _firebaseRepository;
    protected IFirebaseConfiguration? _configuration;
    protected IPhoneDialerService? _phoneService;
    protected DateTime _applicationStartTime;

    public BackgroundService(
        IFirebaseRepository? firebaseRepository,
        IFirebaseConfiguration? configuration,
        IPhoneDialerService? phoneService)
    {
        _firebaseRepository = firebaseRepository;
        _configuration = configuration;
        _phoneService = phoneService;
    }

    public BackgroundService() : this(null, null, null)
    {
        
    }

    public void Start()
    {
        var urlData = new Uri(new Uri(_configuration.FirebaseDataBaseUrl), $"{_configuration.ResourceName}/{_configuration.PhoneNumber}/{_configuration.SecretKey}");
        StartInternal(urlData.AbsoluteUri);
    }

    public void Stop()
    {
        StopInternal();
    }

    partial void StartInternal(string url);

    partial void StopInternal();

    protected virtual void ExecuteProcess(string? urlData)
    {
        _applicationStartTime = DateTime.Now;

        var url = new Uri(urlData);
        var query = url.PathAndQuery;
        var param = query.Split("/");
        var resourceName = param[1];
        var phoneNumber = param[2];
        var secretKey = Guid.Parse(param[3]);
        new FirebaseRepository(url.Scheme + url.Host).RegisterObserver<BarrierOpenMessage>(resourceName, message => Listener(message, secretKey, phoneNumber));
    }

    protected virtual void StopProcess()
    {
        
    }

    private bool Listener(BarrierOpenMessage message, Guid secretKey, string phoneNumber)
    {
        if (message.SecretKey == secretKey && message.RequestDateTime > _applicationStartTime)
        {
            if (PhoneDialer.Default.IsSupported)
            {
                new PhoneDialerService().CallPhone(phoneNumber);
            }
        }

        return true;
    }
}