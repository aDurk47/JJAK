using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    public Player photonPlayer;
    public string unitToSpawn;
    public Transform spawnPoint;

    private Munit selectedUnit;
    public Munit myUnit;

    public static PlayerController me;
    public static PlayerController enemy;

    [PunRPC]
    void Initialize(Player player)
    {
        photonPlayer = player;
        if(player.IsLocal)
        {
            me = this;
            SpawnUnits();
        }
        else
        {
            enemy = this;
        }
    }

    void SpawnUnits()
    {
        GameObject unit = PhotonNetwork.Instantiate(unitToSpawn, spawnPoint.position, Quaternion.identity);
        unit.GetPhotonView().RPC("Initialize", RpcTarget.Others, false);
        unit.GetPhotonView().RPC("Initialize", photonPlayer, true);
    }

    public void BeginTurn()
    {
        myUnit.usedThisTurn = false;
    }

    public void OnAttackButton()
    {
        if(myUnit.usedThisTurn)
            return;
        myUnit.Attack();
    }

    public void OnSpellButton(int spellnum)
    {
        if(myUnit.usedThisTurn)
            return;
        myUnit.CastSpell(spellnum);
    }

    public void onDefendButton()
    {
        
    }
        
    public void onRunButton()
    {
        me.myUnit.curHealth = 0;
        GameUI.instance.UpdateHud();
        GameManager.instance.CheckWinCondition();
    }

    public void EndTurn()
    {
        GameManager.instance.photonView.RPC("SetNextTurn", RpcTarget.All);
    }
}
