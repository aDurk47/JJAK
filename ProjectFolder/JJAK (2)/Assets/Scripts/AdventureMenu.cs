using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AdventureMenu : MonoBehaviourPunCallbacks
{
    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;
    [Header("Main Screen")]
    public Button playButton;
    [Header("Lobby Screen")]
    public Text player1;
    public Text player2;
    public Text gameStartingText;

    private bool connected = false;

    void Start()
    {
        playButton.interactable = false;
    }

    void Update()
    {
        playButton.interactable = connected;
    }

    public override void OnConnectedToMaster ()
    {
        connected = true;
    }

    public void SetScreen (GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void OnPlayButton()
    {
        NetworkManager.instance.CreateOrJoinRoom();
    }

    public void OnPlaySingleButton()
    {
        NetworkManager.instance.loadSinglePlayer(true);
    }

    public override void OnJoinedRoom()
    {
        SetScreen(lobbyScreen);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }

    [PunRPC]
    void UpdateLobbyUI()
    {
        player1.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        /*Invoke("TryStartGame", 3.0f);*/
        player2.text = PhotonNetwork.PlayerList.Length == 2 ? PhotonNetwork.CurrentRoom.GetPlayer(2).NickName : "...";

        if(PhotonNetwork.PlayerList.Length == 2)
        {
            
            player2.gameObject.SetActive(true);

            gameStartingText.gameObject.SetActive(true);

            if(PhotonNetwork.IsMasterClient)
                Invoke("TryStartGame", 3.0f);
        }
    }

    void TryStartGame()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
            NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "MultiplayerGame");
        else
            gameStartingText.gameObject.SetActive(false);
    }

    public void OnLeaveButton()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateLobbyUI();
    }
}