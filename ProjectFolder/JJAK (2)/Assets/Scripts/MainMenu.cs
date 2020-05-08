using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text ClassDescription;
    public Button Confirm;
    public Image image;

    void Start()
    {
        Confirm.interactable = false;
    }
    public void QuitGame ()
    {
        Debug.Log ("QUIT");
        Application.Quit();
    }
    public void changeScenes()
    {
        NetworkManager.instance.loadCharacterCreation(false);
    }
    public void getDescription(CharacterClass input)
    {
        image.gameObject.SetActive(true);
        ClassDescription.text = input.characterClassDescription + "\r\n\n"
            + "Strength Bonus:\t" + input.strength + "\r\n"
            + "Wisdom Bonus:\t" + input.wisdom + "\r\n"
            + "Agility Bonus:\t" + input.agility + "\r\n";
        image.sprite = input.front;
    }
}
