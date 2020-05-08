using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public string job, name, skin;
    public int str, wis, agi, style, slot = 0;

    public Character(int s)
    {
        slot = s;
    }

    public void SetSlot(int s)
    {
        slot = s;
    }

    public void SetJob(string j)
    {
        job = j;
    }

    public string GetJob()
    {
        return job;
    }
    
    public void SetName(string n)
    {
        name = n;
    }

    public string GetName()
    {
        return name;
    }
    
    public void SetSkin(string s)
    {
        skin = s;
    }

    public string GetSkin()
    {
        return skin;
    }
    
    public void SetStats(int s, int w, int a)
    {
        str = s;
        wis = w;
        agi = a;
    }

    public int GetStr()
    {
        return str;
    }
    
    public int GetWis()
    {
        return wis;
    }
    
    public int GetAgi()
    {
        return agi;
    }
    
    public void SetStyle(int s)
    {
        style = s;
    }

    public int GetStyle()
    {
        return style;
    }
    
    public int GetSlot()
    {
        return slot;
    }
}
