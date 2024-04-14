﻿using BarrierOpener.Domain.Core;
using Firebase.Database;
using Firebase.Database.Query;

namespace BarrierOpener.Domain.Repository;

public class FirebaseRepository : IFirebaseRepository
{
    private readonly FirebaseClient _firebaseClient;

    public FirebaseRepository(IFirebaseConfiguration configuration) : this(configuration.FirebaseDataBaseUrl)
    {
    }

    public FirebaseRepository(string baseUrl)
    {
        _firebaseClient = new FirebaseClient(baseUrl);
    }

    public void RegisterObserver<T>(string resourceName, Func<T, bool> messageHandler)
    {
        _firebaseClient
            .Child(resourceName)
            .AsObservable<T>()
            .Subscribe(item =>
            {
                var toDeleteIt = true;
                if (item.Object != null)
                    toDeleteIt = messageHandler(item.Object);
                
                if(toDeleteIt)
                    _firebaseClient.Child(resourceName).Child(item.Key).DeleteAsync().GetAwaiter().GetResult();
            });
    }

    public void SendMessage<T>(string resourceName, T message)
    {
        _firebaseClient.Child(resourceName)
            .PostAsync(message)
            .GetAwaiter().GetResult();
    }
}