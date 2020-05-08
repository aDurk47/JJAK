using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Customizer : MonoBehaviour
{
    public Color[] colors;
    public Sprite[] styles;
    public Image box;
    public Image person;
    public Text styleText;
    private int curColor = 0;
    private int curStyle = 0;

    void Awake()
    {
        person.color = colors[curColor];
        box.color = colors[curColor];
    }

    public void Increase(bool isColor)
    {
        if(isColor)
        {
            
            curColor = (curColor+1) % colors.Length;

        }
        else
        {
            curStyle = (curStyle+1)% styles.Length;
        }
    }

    public void Decrease(bool isColor )
    {
        if (isColor)
        {
            curColor = (curColor+colors.Length-1) % colors.Length;
        }
        else
        {
            curStyle = (styles.Length+curStyle-1) % styles.Length;
        }

    }
    public Color getColor()
    {
        return colors[curColor];
    }

    public Sprite getStyle()
    {
        return styles[curStyle];
    }
    void Update()
    {
        person.color = colors[curColor];
        box.color = colors[curColor];
        styleText.text = "" + curStyle;
    }
}
