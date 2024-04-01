namespace BarrierOpener.Server.Services;

public partial class PhoneDialerService
{
    public void CallPhone(string number)
    {
        CallPhoneInternal(number);
    }

    partial void CallPhoneInternal(string number);
}