using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    
    void Awake()
    {
        if(NetworkManager.instance == null)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateOrJoinRoom()
    {
        if(PhotonNetwork.CountOfRooms > 0)
            PhotonNetwork.JoinRandomRoom();
        else
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;

            PhotonNetwork.CreateRoom(null, options);
        }
    }

    [PunRPC]
    public void ChangeScene (string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
