using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    public PlayerController leftPlayer;
    public PlayerController rightPlayer;
    public PlayerController curPlayer;

    public float postGameTime;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            SetPlayers();
    }

    void SetPlayers()
    {
        leftPlayer.photonView.TransferOwnership(1);
        rightPlayer.photonView.TransferOwnership(2);
        leftPlayer.photonView.RPC("Initialize",RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1));
        rightPlayer.photonView.RPC("Initialize",RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2));

        photonView.RPC("SetNextTurn",RpcTarget.AllBuffered);

    }

    [PunRPC]
    void SetNextTurn()
    {
        if(curPlayer == null)
            curPlayer = leftPlayer;
        else
            curPlayer = curPlayer == leftPlayer ? rightPlayer : leftPlayer;

        if(curPlayer == PlayerController.me)
            PlayerController.me.BeginTurn();

        GameUI.instance.ToggleEndTurnButton(curPlayer == PlayerController.me);
    }

    public PlayerController GetOtherPlayer(PlayerController player)
    {
        return player == leftPlayer ? rightPlayer : leftPlayer;
    }

    public void CheckWinCondition ()
    {
        if(PlayerController.me.myUnit.curHealth < 0)
            photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 0 : 1);
    }

    [PunRPC]
    void WinGame (int winner)
    {
        PlayerController winningPlayer = winner == 0 ? leftPlayer : rightPlayer;
        //Set winning text with winningPlayer

        // go back to the menu after a few seconds
        Invoke("GoBackToMenu", postGameTime);
    }
        
    public void onRunButton()
    {
        GoBackToMenu();
    }

    // leave the room and go back to the menu
    void GoBackToMenu ()
    {
        PhotonNetwork.LeaveRoom();
        NetworkManager.instance.ChangeScene("Menu");
    }
}
