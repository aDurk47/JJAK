using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    public Unit playerUnit;
    public Munit playerMunit;
    public Text text;
    public string PlayerName;
    public CharacterClass PlayerClass;

    public int Strength;
    public int Wisdom;
    public int Agility;

    public Text classText;

    public void confirmStats(int [] stats)
    {
        Strength = PlayerClass.strength + stats[0];
        Wisdom = PlayerClass.wisdom + stats[1];
        Agility = PlayerClass.agility + stats[2];
    }

    void Update()
    {
        classText.text = Strength + "\n" + Wisdom + "\n" + Agility;
    }

    public void confirmInfo(CharacterClass Input)
    {
        PlayerClass = Input;   
    }

    public void getName(string text)
    {
        PlayerName = text;
    }

    public void sendPlayer()
    {
        sendToUnit();
        sendToMunit();
    }

    void sendToUnit()
    {
        playerUnit.unitName = PlayerName;
        playerUnit.front = PlayerClass.front;
        playerUnit.back = PlayerClass.back;
        playerUnit.damage = Strength;
        playerUnit.might = Wisdom;
        playerUnit.agility = Agility;
        playerUnit.spells = PlayerClass.spells;
    }
    void sendToMunit()
    {
        playerMunit.unitName = PlayerName;
        playerMunit.damage = Strength;
        playerMunit.might = Wisdom;
        playerMunit.agility = Agility;
        playerMunit.front = PlayerClass.front;
        playerMunit.back = PlayerClass.back;
    }
}
