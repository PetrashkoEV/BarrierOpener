using BarrierOpener.Domain.Core;
using Microsoft.Extensions.Configuration;

namespace BarrierOpener.Domain.Configuration;

public class FirebaseConfiguration : IFirebaseConfiguration
{
    public const string ConfigurationKey = "FirebaseConfiguration";

    public FirebaseConfiguration(IConfiguration configuration)
    {
        configuration.GetRequiredSection(ConfigurationKey).Bind(this);
    }

    public string FirebaseDataBaseUrl { get; set; }
    public string ResourceName { get; set; }
    public Guid SecretKey { get; set; }
    public string PhoneNumber { get; set; }
}