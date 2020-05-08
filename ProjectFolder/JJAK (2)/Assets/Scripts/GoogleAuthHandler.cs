using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;

public static class GoogleAuthHandler
{
    private static string clientId = "527099369878-psrpsb0h4qqv78j2h14t890dnv2ef0j9.apps.googleusercontent.com";
    private static string clientSecret = "RQ9v3CATEVr9FBKAYJhoZ5LV";
    
    public static void GetUserCode()
    {
        Application.OpenURL($"https://accounts.google.com/o/oauth2/v2/auth?client_id={clientId}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&response_type=code&scope=email");
    }

    public static void ExchangeAuthCodeWithIDToken(string code, Action<string> callback)
    {
        RestClient.Post(
            $"https://oauth2.googleapis.com/token?code={code}&client_id={clientId}&client_secret={clientSecret}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&grant_type=authorization_code",
            null).Then(
            response =>
            {
                var data = StringSerializationAPI.Deserialize(typeof(GoogleIdTokenResponse), response.Text) as GoogleIdTokenResponse;
                callback(data.id_token);
            });
    }
}
