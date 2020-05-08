using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMaker : MonoBehaviour
{
    public Image image;
    public Text Name;
    public Unit unit;
    
    void Awake()
    {
        OnAddClick();
    }

    void Update()
    {
        OnAddClick();
    }

    public void OnAddClick()
    {
        image.sprite = unit.front;
        Name.text = unit.unitName;
    }

    public void OnDeleteClick()
    {
        this.gameObject.SetActive(false);
        Name.text = "";
    }
}
