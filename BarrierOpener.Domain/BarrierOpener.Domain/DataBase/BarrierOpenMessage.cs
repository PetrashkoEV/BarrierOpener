namespace BarrierOpener.Domain.DataBase;

public class BarrierOpenMessage
{
    public Guid SecretKey { get; set; }
    public DateTime RequestDateTime { get; set; }
    public string DeviceName { get; set; } = string.Empty;
}