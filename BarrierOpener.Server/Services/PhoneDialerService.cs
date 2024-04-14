using BarrierOpener.Domain.Core;

namespace BarrierOpener.Server.Services;

public partial class PhoneDialerService : IPhoneDialerService
{
    public void CallPhone(string number)
    {
        CallPhoneInternal(number);
    }

    partial void CallPhoneInternal(string number);
}