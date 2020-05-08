using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : MonoBehaviour
{
    public string characterClassName;
    public string characterClassDescription;
    public Sprite front;
    public Sprite back;
    public Spell[] spells;

    //stats
    public int strength;
    public int wisdom;
    public int agility;
}
