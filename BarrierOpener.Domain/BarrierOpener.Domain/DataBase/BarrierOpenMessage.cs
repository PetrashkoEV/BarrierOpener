namespace BarrierOpener.Domain.DataBase;

public class BarrierOpenMessage
{
    public DateTime RequestDateTime { get; set; }
    public string DeviceName { get; set; } = string.Empty;
}