using BarrierOpener.Domain.Core;
using BarrierOpener.Domain.DataBase;
using BarrierOpener.Server.Core;

namespace BarrierOpener.Server.Services;

public class FirebaseListenerService : IFirebaseListenerService
{
    private DateTime _applicationStartTime;

    private readonly IPhoneDialerService _phoneService;
    private readonly IFirebaseRepository _firebaseRepository;
    private readonly IFirebaseConfiguration _configuration;

    public FirebaseListenerService(
        IPhoneDialerService phoneService, 
        IFirebaseRepository firebaseRepository, 
        IFirebaseConfiguration configuration)
    {
        _phoneService = phoneService;
        _firebaseRepository = firebaseRepository;
        _configuration = configuration;
        MessageNotification = null;
    }

    public Action<BarrierOpenMessage>? MessageNotification { get; set; }

    public void RegisterListener(DateTime startTime)
    {
        _applicationStartTime = startTime;
        _firebaseRepository.RegisterObserver<BarrierOpenMessage>(_configuration.ResourceName, Listener);
    }

    private bool Listener(BarrierOpenMessage message)
    {
        if (message.RequestDateTime > _applicationStartTime)
        {
            if (PhoneDialer.Default.IsSupported)
            {
                _phoneService.CallPhone(_configuration.PhoneNumber);
            }
            MessageNotification?.Invoke(message);
        }

        return true;
    }
}