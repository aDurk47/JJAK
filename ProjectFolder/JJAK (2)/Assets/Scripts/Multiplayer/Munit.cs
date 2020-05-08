using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Munit : MonoBehaviourPun
{
    private System.Random rand = new System.Random();
    public string unitName;
    public int Health;
    public int curHealth;
    public int Mana;
    public int curMana;
    public int damage;
    public int agility;
    public int might;
    public Spell[] spells;
    public bool usedThisTurn;
    public SpriteRenderer frontRenderer;
    public SpriteRenderer backRenderer;
    public Sprite front;
    public Sprite back;
    private double shieldAmount = 0;
    private int shieldTime = 0;

    [PunRPC]
    void Initialize (bool isMine)
    {
        frontRenderer.sprite = front;
        backRenderer.sprite = back;
        if(isMine)
        {
            PlayerController.me.myUnit = this;
            GameUI.instance.SetPlayerText(PlayerController.me);
        }
        else
        {
            GameManager.instance.GetOtherPlayer(PlayerController.me).myUnit = this;
            GameUI.instance.SetPlayerText(GameManager.instance.GetOtherPlayer(PlayerController.me));
        }
    }

    public void disable(bool left)
    {
        if(left)
            backRenderer.gameObject.SetActive(false);
        else
            frontRenderer.gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        curHealth += amount;
        if(curHealth > Health)
            curHealth = Health;
    }

    public void GainMana(int amount)
    {
        curMana += amount;
        if(curMana > Mana)
            curMana = Mana;
    }

    public bool canCast(int cost)
    {
        if(curMana < cost)
            return false;
        curMana -= cost;
        return true;
    }

    public void Defend()
    {
        usedThisTurn = true;
        Heal(15);
        GainMana(15);
        shieldTime = 1;
        shieldAmount = .25;

        GameUI.instance.Print("You feel renewed strength!");
        PlayerController.me.EndTurn();
    }

    public void CastSpell(int spellnumber)
    {
        Spell spell = spells[spellnumber];
        if(!canCast(spell.manaCost))
        {
            GameUI.instance.Print("You do not have enough mana for this spell.");
            return;
        }
        usedThisTurn = true;
        GameUI.instance.Print("You cast " + spell.spellname + " which costs " + spell.manaCost + " mana");
        
        if(spell.damage > 0)
        {
            float dmg = spell.damage + might;
            PlayerController.enemy.myUnit.photonView.RPC("TakeDamage", RpcTarget.All, (int)dmg);
        }
        PlayerController.me.EndTurn();
    }

    [PunRPC]
    void Die()
    {
        if(photonView.IsMine)
            GameManager.instance.CheckWinCondition();
    }

    [PunRPC]
    public void Attack()
    {
        usedThisTurn = true;
        int chance = rand.Next(0,100);

        double dmg = damage + 15;
        string message = "";
        if(chance < 20)
        {
            dmg *= .8;
            message = "Barely hit. ";
        }
        else if(chance > 80)
        {
            dmg *= 1.2;
            message = "A crushing blow! ";
        }

        message += PhotonNetwork.NickName + " dealt " + (int)dmg + " damage.";
        GameUI.instance.Print(message);
        PlayerController.enemy.myUnit.photonView.RPC("TakeDamage", RpcTarget.All, (int)dmg);
        PlayerController.me.EndTurn();
    }

    [PunRPC]
    public void TakeDamage(int dmg)
    {
        double damageDone = dmg;
        if(shieldTime > 0)
        {
            GameUI.instance.Print("Your shield blocked some damage!");
            damageDone *= (1 - shieldAmount);
            shieldTime--;
        }
        GameUI.instance.Print(this.name + " took " + (int)damageDone + " damage.");
        curHealth -= (int)damageDone;
        if (curHealth <= 0)
            photonView.RPC("Die", RpcTarget.All);
        else
        {
            photonView.RPC("UpdateHealthBar", RpcTarget.All);
        }
    }

    // updates the UI health bar
    [PunRPC]
    void UpdateHealthBar()
    {
        GameUI.instance.UpdateHud();
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
