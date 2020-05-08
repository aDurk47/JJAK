using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using Photon.Pun.Demo.Cockpit;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class FirebaseAuthHandler
{
    private const string apiKey = "AIzaSyC-oP5a_BySYNp7mlsuNcnzCoK7m0GFxbg";
    private static User currentUser;
    private static Character currentChar;
    public static void SignInWithToken(string idToken, string providerId)
    {
        var payload = $"{{\"postBody\":\"id_token={idToken}&providerId={providerId}\",\"requestUri\":\"http://localhost\",\"returnIdpCredential\":true,\"returnSecureToken\":true}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={apiKey}", payload).Then(
            response =>
            {
                PostUserToDatabase();
                SceneManager.LoadScene("Menu");
                Debug.Log(response.Text);
                
            });
    }

    public static void PostUserToDatabase()
    {
        currentUser = new User();
        RestClient.Put("https://jjak-b5bbc.firebaseio.com/users/" + currentUser.uid + ".json", currentUser).Then(
            response =>
            {
                Debug.Log(response.Text);
            });
    }
    
    public static void PostCharacterToDatabase()
    {
        currentChar = new Character(0);
        RestClient.Put("https://jjak-b5bbc.firebaseio.com/users/" +  currentUser.uid + "/character" + currentChar.GetSlot() + ".json", currentChar).Then(
            response =>
            {
                Debug.Log(response.Text);
            });
    }
}
