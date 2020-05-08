using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Munit : MonoBehaviourPun
{
    public string unitName;
    public int Health;
    public int curHealth;
    public int Mana;
    public int curMana;
    public int damage;
    //public int agility;
    //public int might;
    public Spell[] spells;
    public bool usedThisTurn;
    public SpriteRenderer frontRenderer;
    public SpriteRenderer backRenderer;
    public Sprite front;
    public Sprite back;

    [PunRPC]
    void Initialize (bool isMine)
    {
        if(isMine)
        {
            PlayerController.me.myUnit = this;
            frontRenderer.sprite = front;
            //PlayerController.me.hud.SetHudWithMunit(this);
        }
        else
        {
            GameManager.instance.GetOtherPlayer(PlayerController.me).myUnit = this;
            backRenderer.sprite = back;
        }
    }

    [PunRPC]
    public void TakeDamage(int dmg)
    {
        curHealth -= dmg;
        if (curHealth <= 0)
            photonView.RPC("Die", RpcTarget.All);
        else
            photonView.RPC("UpdateHealthBar", RpcTarget.All, curHealth);
    }

    public void Heal(int amount)
    {
        curHealth += amount;
        if(curHealth > Health)
            curHealth = Health;
    }
    public bool CastSpell(int cost)
    {
        if(curMana < cost)
            return false;
        curMana -= cost;
        return true;
    }

    public void GainMana(int amount)
    {
        curMana += amount;
        if(curMana > Mana)
            curMana = Mana;
    }

    [PunRPC]
    void Die()
    {

    }

    [PunRPC]
    public void Attack(Unit unitToAttak)
    {
        //unitToAttack.photonView.RPC("TakeDamage", );
    }

    public bool isDead()
    {
        return curHealth <= 0;
    }

    /*public IEnumerator attackAnimation()
    {
        animator.SetBool("Attacked", true);
        yield return new WaitForSeconds(.66f);
        animator.SetBool("Attacked", false);
    }*/


}
