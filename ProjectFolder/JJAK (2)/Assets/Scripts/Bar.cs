using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMax(int amt)
    {
        slider.maxValue = amt;
        slider.value = amt;

        fill.color = gradient.Evaluate(1f);
    }


    // Start is called before the first frame update
    public void Set(int amt)
    {
        slider.value = amt;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public int Get()
    {
        return (int) slider.value;
    }
}
