using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    public PlayerController APlayer;
    public PlayerController BPlayer;
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
        APlayer.photonView.TransferOwnership(1);
        BPlayer.photonView.TransferOwnership(2);
        APlayer.photonView.RPC("Initialize",RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1));
        BPlayer.photonView.RPC("Initialize",RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2));

        photonView.RPC("SetNextTurn",RpcTarget.AllBuffered);

    }

    public void HidePlayers(bool hide)
    {
        PlayerController.me.gameObject.SetActive(!hide);
        PlayerController.enemy.gameObject.SetActive(!hide);
    }

    [PunRPC]
    void SetNextTurn()
    {
        if(curPlayer == null)
            curPlayer = APlayer;
        else
            curPlayer = curPlayer == APlayer ? BPlayer : APlayer;
        GameUI.instance.Print(curPlayer.photonPlayer.NickName + "'s Turn.");
        if(curPlayer == PlayerController.me)
            PlayerController.me.BeginTurn();

        GameUI.instance.ToggleEndTurnButton(curPlayer == PlayerController.me);
    }

    public PlayerController GetOtherPlayer(PlayerController player)
    {
        return player == APlayer ? BPlayer : APlayer;
    }

    public void CheckWinCondition ()
    {
        if(PlayerController.me.myUnit.curHealth < 0)
            photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == APlayer ? 0 : 1);
    }

    [PunRPC]
    void WinGame (int winner)
    {
        PlayerController winningPlayer = winner == 0 ? APlayer : BPlayer;
        //Set winning text with winningPlayer
        HidePlayers(true);
        GameUI.instance.SetWinText(winningPlayer.photonPlayer.NickName);
        // go back to the menu after a few seconds
        Invoke("GoBackToMenu", postGameTime);
    }

    // leave the room and go back to the menu
    void GoBackToMenu ()
    {
        PhotonNetwork.LeaveRoom();
        NetworkManager.instance.ChangeScene("Menu");
    }
}
