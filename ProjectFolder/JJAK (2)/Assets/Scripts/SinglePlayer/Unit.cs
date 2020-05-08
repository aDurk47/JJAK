using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Animator animator;
    public Sprite front;
    public Sprite back;
    public SpriteRenderer spriteRenderer;
    public string unitName;
    public int Health;
    public int curHealth;
    public int Mana;
    public int curMana;
    public int damage;
    public int agility;
    public int might;
    public Spell[] spells;

    public void makeSprite()
    {
        spriteRenderer.sprite = back;
    }

    public bool TakeDamage(int dmg)
    {
        curHealth -= dmg;
        return isDead();
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

    public bool isDead()
    {
        return curHealth <= 0;
    }

    public IEnumerator attackAnimation()
    {
        animator.SetBool("Attacked", true);
        yield return new WaitForSeconds(.66f);
        animator.SetBool("Attacked", false);
    }
}
