using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text playerName;
    public Bar healthBar;
    public Bar manaBar;
    public Text healthNum;
    public Text manaNum;

    public void SetHud(Unit unit)
    {
        playerName.text = unit.unitName;
        healthBar.SetMax(unit.Health);
        manaBar.SetMax(unit.Mana);
        healthNum.text = ""+unit.Health;
        manaNum.text = ""+unit.Mana;
    }

    public void SetHudWithMunit(Munit unit)
    {
        playerName.text = unit.nickName;
        healthBar.SetMax(unit.Health);
        manaBar.SetMax(unit.Mana);
        healthNum.text = ""+unit.Health;
        manaNum.text = ""+unit.Mana;
    }

    public void SetHP(int hp)
    {
        healthBar.Set(hp);
        healthNum.text = ""+hp;
        if(hp < 0)
            healthNum.text = "0";
    }

    public void SetMana(int mana)
    {
        manaBar.Set(mana);
        manaNum.text = ""+mana;
    }
}
