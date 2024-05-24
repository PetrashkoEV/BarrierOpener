using BarrierOpener.Domain.DataBase;

namespace BarrierOpener.Server.Core;

public interface IFirebaseListenerService
{
    Action<BarrierOpenMessage>? MessageNotification { get; set; }
    void RegisterListener(DateTime startTime);
}