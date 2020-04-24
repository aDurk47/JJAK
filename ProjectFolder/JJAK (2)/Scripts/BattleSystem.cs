using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum BattleState {
    START,
    WAIT,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviourPun
{
    public BattleState state;
    public GameObject enemyPrefab;
    public GameObject player1Prefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    public Hud enemyHud;
    public Hud playerHud;
    public Text dialogueText;
    public Text descriptionText;
    public Text manaCostText;
    public Text[] spellNames;
    private bool playersTurn = false;
    float time = 1.5f;
    Unit player1;
    Unit enemyUnit;
    private int skipTurn = 0;
    private int shieldTime = 0;
    private double shieldAmount = 0;
    private bool isStun = false;
    System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        player1 = Instantiate(player1Prefab, playerBattleStation).GetComponent<Unit>();
        player1.makeSprite();
        playerHud.SetHud(player1);

        enemyUnit = Instantiate(enemyPrefab, enemyBattleStation).GetComponent<Unit>();
        enemyHud.SetHud(enemyUnit);

        for(int i =0; i < 3; i++)
            spellNames[i].text = player1.spells[i].spellname;
        
        yield return new WaitForSeconds(time);

        int[] turns = {enemyUnit.agility, player1.agility};
        while(state != BattleState.WON && state != BattleState.LOST)
        {
            if(turns[1] == 0)
            {
                player1.GainMana(player1.might);
                playerHud.SetMana(player1.curMana);
                if(skipTurn > 0)
                {
                    dialogueText.text = "You are still recovering from the spell.";
                    yield return new WaitForSeconds(time);
                    skipTurn--;
                }
                else
                {
                    playersTurn = true;
                    state = BattleState.PLAYERTURN;
                    yield return StartCoroutine(PlayerTurn());
                    if(enemyUnit.isDead())
                    {
                        state = BattleState.WON;
                        StartCoroutine(EndBattle());
                    }
                    if(state == BattleState.LOST)
                        StartCoroutine(EndBattle());
                }
                turns[1] = (20 - player1.agility);
            }

            if(turns[0] == 0)
            {
                turns[0] = (20 - enemyUnit.agility);
                if(isStun)
                {
                    dialogueText.text = "The enemy is stunned.";
                    yield return new WaitForSeconds(time);
                    isStun = false;
                }
                else{
                    state = BattleState.ENEMYTURN;
                    yield return StartCoroutine(EnemyTurn());
                }
            }

            turns[0]--;
            turns[1]--;
        }
        StartCoroutine(EndBattle());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return StartCoroutine(enemyUnit.attackAnimation());
        double dmg = rand.Next(10, 25);

        if(shieldTime > 0)
        {
            dialogueText.text = "Your shield blocked some damage!";
            yield return new WaitForSeconds(time);
            dmg *= (1 - shieldAmount);
            shieldTime--;
        }

        player1.TakeDamage((int)dmg);
        playerHud.SetHP(player1.curHealth);
        dialogueText.text = player1.unitName + " took " + (int)dmg + " damage.";
        yield return new WaitForSeconds(time);

        if(player1.isDead())
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
    }


    IEnumerator PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
        yield return new WaitUntil(() => !playersTurn);
    }

    IEnumerator PlayerAttack()
    {
        int chance = rand.Next(0,100);

        double dmg = player1.damage + 15;
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

        dialogueText.text = message + enemyUnit.unitName + " took " + (int)dmg + " damage.";
        yield return StartCoroutine(player1.attackAnimation());

        enemyUnit.TakeDamage((int)dmg);
        enemyHud.SetHP(enemyUnit.curHealth);

        yield return new WaitForSeconds(time);
        playersTurn = false;
    }

    IEnumerator PlayerDefend()
    {
        player1.Heal(15);
        playerHud.SetHP(player1.curHealth);
        player1.GainMana(15);
        playerHud.SetMana(player1.curMana);
        shieldTime = 1;
        shieldAmount = .25;

        dialogueText.text = "You feel renewed strength!";
        yield return new WaitForSeconds(time);
        playersTurn = false;
    }

    IEnumerator PlayerSpells(int spellnumber)
    {
        Spell spell = player1.spells[spellnumber - 1];
        if(!player1.CastSpell(spell.manaCost))
        {
            dialogueText.text = "You do not have enough mana for this spell.";
            state = BattleState.PLAYERTURN;
            yield break;
        }
        
        dialogueText.text = "You cast " + spell.spellname + " which costs " + spell.manaCost + " mana";
        playerHud.SetMana(player1.curMana);
        yield return new WaitForSeconds(time);
        
        if(spell.damage > 0)
        {
            yield return StartCoroutine(player1.attackAnimation());
            float dmg = spell.damage + player1.might;
            enemyUnit.TakeDamage((int)dmg);
            enemyHud.SetHP(enemyUnit.curHealth);
            dialogueText.text = enemyUnit.unitName + " took " + dmg + " damage.";
            yield return new WaitForSeconds(time);
            if(enemyUnit.isDead())
                StartCoroutine(EndBattle());
        }

        if(spell.heal > 0)
        {
            player1.Heal(spell.heal + player1.might);
            playerHud.SetHP(player1.curHealth);
            dialogueText.text = "You feel renewed strength!";
            yield return new WaitForSeconds(time);
        }
        
        if(spell.shield > 0)
        {
            shieldTime = spell.turnDuration;
            shieldAmount = spell.shield;
            dialogueText.text = "You are bracing for an attack.";
            yield return new WaitForSeconds(time);
        }

        if(spell.selfDamage > 0)
        {
            int chance = rand.Next(0,100);
            if(chance < 40)
            {
                player1.TakeDamage(spell.selfDamage);
                playerHud.SetHP(player1.curHealth);
                dialogueText.text = "The spell backfired! You took " + spell.selfDamage + " damage.";
                yield return new WaitForSeconds(time);

                if(player1.isDead())
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());
                }
            }
        }

        if(spell.stun)
            isStun = true;

        skipTurn = spell.turnsCost - 1;

        playersTurn = false;
    }

    IEnumerator PlayerRun()
    {
        dialogueText.text = "You ran from the battle... Coward";
        yield return new WaitForSeconds(time);

        playersTurn = false;
        state = BattleState.LOST;
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        state = BattleState.WAIT;
        StartCoroutine(PlayerAttack());
    }

    public void OnDefendButton()
    {
        if(state != BattleState.PLAYERTURN)
        return;
        state = BattleState.WAIT;
        StartCoroutine(PlayerDefend());
    }
    
    public void OnSpellsButton(int spellnumber)
    {
        if(state != BattleState.PLAYERTURN)
            return;
        state = BattleState.WAIT;
        StartCoroutine(PlayerSpells(spellnumber));
    }

    public void OnRunButton()
    {
        if(state != BattleState.PLAYERTURN)
            return;
        state = BattleState.WAIT;
        StartCoroutine(PlayerRun());
    }

    public void ChangeDescription(int spellnumber)
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
        manaCostText.text = manaCostAmount;
    }


    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        }

        yield return new WaitForSeconds(time);
        NetworkManager.instance.ChangeScene("Menu");
    }
}
