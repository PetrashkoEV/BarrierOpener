namespace BarrierOpener.Domain.Core;

public interface IFirebaseRepository
{
    void RegisterObserver<T>(string resourceName, Func<T, bool> messageHandler);
    void SendMessage<T>(string resourceName, T message);
}