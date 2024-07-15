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
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void SettingsButton()
    {
        _audioSource.Play();
        openSettingsMenu.SetActive(true);
        SettingsMenu = true;
    }

    public void BackToMenu()
    {
        _audioSource.Play();
        openSettingsMenu.SetActive(false);
        SettingsMenu = false;
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void StartButton()
    {
        _audioSource.Play();
        ChangeScene();
    }
}