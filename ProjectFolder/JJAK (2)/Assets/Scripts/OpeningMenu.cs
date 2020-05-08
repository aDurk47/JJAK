using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningMenu : MonoBehaviour
{
    public Unit unit;
    public Munit munit;
    public CharacterClass[] classes;

    const string unitNamePrefKey = "nameUnit";
    const string classPrefKey = "class";
    const string strengthPrefKey = "strength";
    const string wisdomPrefKey = "wisdom";
    const string agilityPrefKey = "agility";

    void Start()
    {
        if (PlayerPrefs.HasKey(unitNamePrefKey))
        {
            int classNum = PlayerPrefs.GetInt(classPrefKey);

            unit.unitName = PlayerPrefs.GetString(unitNamePrefKey);
            unit.front = classes[classNum].front;
            unit.back = classes[classNum].back;
            unit.damage = PlayerPrefs.GetInt(strengthPrefKey);
            unit.might = PlayerPrefs.GetInt(wisdomPrefKey);
            unit.agility = PlayerPrefs.GetInt(agilityPrefKey);
            unit.spells = classes[classNum].spells;

            munit.unitName = PlayerPrefs.GetString(unitNamePrefKey);
            munit.front = classes[classNum].front;
            munit.back = classes[classNum].back;
            munit.damage = PlayerPrefs.GetInt(strengthPrefKey);
            munit.might = PlayerPrefs.GetInt(wisdomPrefKey);
            munit.agility = PlayerPrefs.GetInt(agilityPrefKey);
            munit.spells = classes[classNum].spells;
        }
    }
}