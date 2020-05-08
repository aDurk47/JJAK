﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Hud leftPlayerHud;
    public Hud rightPlayerHud;
    public Text dialogText;
    public Text descriptionText;
    public Text manaText;
    public GameObject actionsTab;
    public GameObject spellsTab;
    public GameObject WinningSlide;
    public Text WinningText;
    public Munit player1;
    public Text[] spells;

    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        spells[0].text = player1.spells[0].spellname;
        spells[1].text = player1.spells[1].spellname;
        spells[2].text = player1.spells[2].spellname;
    }

    public void OnAttackButton() { PlayerController.me.OnAttackButton(); }
    public void OnDefendButton() { PlayerController.me.onDefendButton(); }
    public void OnSpellButton(int i) { PlayerController.me.OnSpellButton(i); }
    public void OnRunButton() { PlayerController.me.onRunButton(); }

    public void OnEndTurn()
    {
        PlayerController.me.EndTurn();
    }

    public void ToggleEndTurnButton (bool toggle)
    {
        actionsTab.SetActive(toggle);
        spellsTab.SetActive(false);
    }

    public void SetPlayerText(PlayerController player)
    {
        Hud hud = player == GameManager.instance.APlayer ? leftPlayerHud : rightPlayerHud;
        hud.SetHudWithMunit(player);
        player.myUnit.disable(player == GameManager.instance.APlayer ? false : true);
    }

    public void UpdateHud()
    {
        leftPlayerHud.SetHP(GameManager.instance.APlayer.myUnit.curHealth);
        leftPlayerHud.SetMana(GameManager.instance.APlayer.myUnit.curMana);
        rightPlayerHud.SetHP(GameManager.instance.BPlayer.myUnit.curHealth);
        rightPlayerHud.SetMana(GameManager.instance.BPlayer.myUnit.curMana);
    }

    public void Print(string x)
    {
        dialogText.text = x;
    }

    public void ToggleDescription(int spellnumber)
    {
        string temp = "";
        string manaCostAmount = "";
        switch(spellnumber)
        {
            case -1:
                temp += "Attack -\nDamage the enemy with a basic attack.";
                temp += "\nDamage: " + (15 + player1.damage);
                break;
            case -2:
                temp += "Defend -\nInstantly restore some health and mana.";
                temp += "\nHeal: 15";
                temp += "\nMana Regen: 15";
                temp += "\nShield: 25% for 1 turn";
                break;
            case -3:
                temp += "Run if you dare, but no loot to spare.";
                break;
            default:
                Spell spell = player1.spells[spellnumber - 1];
                temp += spell.spellname + " -\n" + spell.description;
                if(spell.damage > 0) temp += "\nDamage: " + spell.damage;
                if(spell.heal > 0) temp += "\nHeal: "+ spell.heal;
                if(spell.shield > 0) temp += "\nShield: " + (int)(spell.shield * 100) + "%";
                if(spell.selfDamage > 0) temp += "\nRisk Damage: " + spell.selfDamage;
                manaCostAmount += spell.manaCost;
                break;
        }
        descriptionText.text = temp;
        manaText.text = manaCostAmount;
    }

    public void SetWinText(string winnerName)
    {
        WinningSlide.SetActive(true);
        WinningText.text = winnerName + " wins!"; 
    }
}