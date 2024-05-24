using BarrierOpener.Domain.Core;
using Firebase.Auth;
using Firebase.Database;

namespace BarrierOpener.Domain.Factory;

public class FirebaseClientFactory : IFirebaseClientFactory
{
    private readonly IFirebaseConfiguration _configuration;
    private FirebaseClient? _firebaseClient;
    private readonly FirebaseAuthClient _authClient;
    private string _token;
    private DateTime _expiresIn;

    public FirebaseClientFactory(IFirebaseConfiguration configuration, FirebaseAuthClient authClient)
    {
        _configuration = configuration;
        _authClient = authClient;
        _expiresIn = DateTime.UtcNow;
    }

    public FirebaseClient GetClient()
    {
        if (_firebaseClient != null)
            return _firebaseClient;

        _firebaseClient = new FirebaseClient(baseUrl: _configuration.FirebaseDataBaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = async () =>
            {
                if (!string.IsNullOrEmpty(_token) && _expiresIn > DateTime.UtcNow)
                    return _token;

                var userCredential = await _authClient.SignInWithEmailAndPasswordAsync(_configuration.UserName, _configuration.Password);
                var w = userCredential.User.Credential.Created.ToUniversalTime();
                _expiresIn = userCredential.User.Credential.Created.ToUniversalTime().AddSeconds(userCredential.User.Credential.ExpiresIn);
                _token = userCredential.User.Credential.IdToken;
                return _token;
            }
        });

        return _firebaseClient;
    }
}