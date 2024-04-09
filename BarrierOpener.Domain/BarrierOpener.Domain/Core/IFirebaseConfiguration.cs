namespace BarrierOpener.Domain.Core;

public interface IFirebaseConfiguration
{
    string FirebaseDataBaseUrl { get; set; }
    string ResourceName { get; set; }
    Guid SecretKey { get; set; }
    string PhoneNumber { get; set; }
}