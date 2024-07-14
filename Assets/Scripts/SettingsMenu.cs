using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public static bool SettingGame;
    public GameObject SettingMenu;

    
    public void Music()
    {

    }

    public void Sound()
    {

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("_Menu");
    }

}
