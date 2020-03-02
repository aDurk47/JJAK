using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text playerName;
    public HealthBar healthBar;

    public void SetHud(Unit unit)
    {
        playerName.text = unit.unitName;
        healthBar.SetMaxHealth(unit.Health);
    }

    public void SetHP(int hp)
    {
        healthBar.SetHealth(hp);
    }
}
