using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    public Camera cam;
    
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
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called, created room instead");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2});
    }

    [PunRPC]
    public void ChangeScene (string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void loadCharacterCreation(bool load)
    {
        if(load)
        {
            cam.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("Character Creator", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("Character Creator");
            cam.gameObject.SetActive(true);
        }
    }

    public void loadSinglePlayer(bool load)
    {
        if(load)
        {
            cam.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("SingleplayerGame", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync("SingleplayerGame");
            cam.gameObject.SetActive(true);
        }
    }
}
