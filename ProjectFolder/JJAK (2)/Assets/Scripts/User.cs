using System;
using Firebase.Auth;

[Serializable]
public class User
{
    static FirebaseAuth auth = FirebaseAuth.DefaultInstance;
    FirebaseUser user = auth.CurrentUser;
    public string name;
    public string email;
    public string uid;
    
    public User()
    {
        if (user != null) {
            name = user.DisplayName;
            email = user.Email;
            uid = user.UserId;
        }
    }
}

