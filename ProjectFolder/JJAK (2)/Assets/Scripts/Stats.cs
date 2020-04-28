using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public GameInfo gameInfo;
    public Button button;
    public Text Total;
    private int total = 6;
    private int[] array = {0,0,0};


    void Start()
    {
        Update();
    }
    public void Increase(int stat)
    {
        if (total > 0)
        {
            array[stat]++;
            total--;
        }
        Update();
    }

    public void Decrease(int stat)
    {
        if (array[stat] > 0)
        {
            array[stat]--;
            total++;
        }
        Update();
    }

    void Update()
    {
        Total.text = "" + total;

        gameInfo.confirmStats(array);

        if(total == 0)
            button.interactable = true;
        else
            button.interactable = false;
    }

    public int [] getStats()
    {
        return array;
    }

}
