using BarrierOpener.Domain.Core;
using Firebase.Database;
using Firebase.Database.Query;

namespace BarrierOpener.Domain.Repository;

public class FirebaseRepository : IFirebaseRepository
{
    private readonly IFirebaseClientFactory _clientFactory;

    private FirebaseClient? _firebaseClient;
    private FirebaseClient FirebaseClient => _firebaseClient ??= _clientFactory.GetClient();

    public FirebaseRepository(IFirebaseClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public void RegisterObserver<T>(string resourceName, Func<T, bool> messageHandler)
    {
        FirebaseClient
            .Child(resourceName)
            .AsObservable<T>()
            .Subscribe(item =>
            {
                var toDeleteIt = true;
                if (item.Object != null)
                    toDeleteIt = messageHandler(item.Object);
                
                if(toDeleteIt)
                    FirebaseClient.Child(resourceName).Child(item.Key).DeleteAsync().GetAwaiter().GetResult();
            });
    }

    public void SendMessage<T>(string resourceName, T message)
    {
        FirebaseClient.Child(resourceName)
            .PostAsync(message)
            .GetAwaiter().GetResult();
    }
}