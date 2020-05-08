using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMenu : MonoBehaviour
{
    public void deleteCharacter ()
    {
    }
    public void selectCharacter ()
    {
    }
    public void AddCharacter()
    {
        NetworkManager.instance.loadCharacterCreation(true);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
