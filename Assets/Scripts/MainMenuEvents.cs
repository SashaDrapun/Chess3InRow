using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class MainMenuEvents : MonoBehaviour
{

    public static bool SettingsMenu;
    public GameObject openSettingsMenu;
    public int sceneNumber;
    private AudioSource audioSourceBackgroundMusic;

    private void Awake()
    {
        audioSourceBackgroundMusic = GetComponent<AudioSource>();
        audioSourceBackgroundMusic.Play();

    }
    public void SettingsButton()
    {
        audioSourceBackgroundMusic.Play();
        openSettingsMenu.SetActive(true);
        SettingsMenu = true;
    }

    public void BackToMenu()
    {
        audioSourceBackgroundMusic.Play();
        openSettingsMenu.SetActive(false);
        SettingsMenu = false;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void StartButton()
    {
        audioSourceBackgroundMusic.Play();
        ChangeScene();
    }
}