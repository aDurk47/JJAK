using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public InputField googleCode;

    public void OnClickGoogleCode()
    {
        GoogleAuthHandler.GetUserCode();
    }

    public void OnClickGoogleSignin()
    {
        GoogleAuthHandler.ExchangeAuthCodeWithIDToken(googleCode.text, idToken =>
        {
            FirebaseAuthHandler.SignInWithToken(idToken, "google.com");
        });
    }
}
