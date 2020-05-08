using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameCreation : MonoBehaviour
{
    public Button Confirm;
    public Text text;
    public GameInfo gameinfo;
    // Start is called before the first frame update
    void Start()
    {
        Confirm.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameinfo.getName(text.text);
        if (text.text.Length > 0)
        {
            Confirm.interactable = true;
        }
        else
            Confirm.interactable = false;
    }

    public void toMenu()
    {
        NetworkManager.instance.loadCharacterCreation(false);
    }
}
