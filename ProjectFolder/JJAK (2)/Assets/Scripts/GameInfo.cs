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
        const string unitNamePrefKey = "nameUnit";
        const string classPrefKey = "class";
        const string strengthPrefKey = "strength";
        const string wisdomPrefKey = "wisdom";
        const string agilityPrefKey = "agility";
        PlayerPrefs.SetInt(classPrefKey, getNum(PlayerClass.characterClassName));
        PlayerPrefs.SetString(unitNamePrefKey,PlayerName);
        PlayerPrefs.SetInt(strengthPrefKey,Strength);
        PlayerPrefs.SetInt(wisdomPrefKey,Wisdom);
        PlayerPrefs.SetInt(agilityPrefKey,Agility);
    }

    public int getNum(string s)
    {
        if(s.Equals("Archer")) return 0;
        else if(s.Equals("Barbarian")) return 1;
        else if(s.Equals("Paladin")) return 2;
        else if(s.Equals("Priest")) return 3;
        else if(s.Equals("Rogue")) return 4;
        else return 5;
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
        playerMunit.spells = PlayerClass.spells;
    }
}
