using Firebase.Database;

namespace BarrierOpener.Domain.Core;

public interface IFirebaseClientFactory
{
    FirebaseClient GetClient();
}