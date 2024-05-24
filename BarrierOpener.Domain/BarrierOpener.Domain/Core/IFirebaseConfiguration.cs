namespace BarrierOpener.Domain.Core;

public interface IFirebaseConfiguration
{
    string FirebaseDataBaseUrl { get; set; }
    string ResourceName { get; set; }
    string PhoneNumber { get; set; }
    string UserName { get; set; }
    string Password { get; set; }
}