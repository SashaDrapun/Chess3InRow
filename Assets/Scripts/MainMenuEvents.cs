using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuEvents : MonoBehaviour
{
    public static bool SettingsMenu;
    public GameObject openSettingsMenu;
    public int sceneNumber;
    private AudioSource audioSourceBackgroundMusic;
    private AudioSource onButtonClick;


    private void Awake()
    {
        List<AudioSource> audioSources = GetComponents<AudioSource>().ToList();
        audioSourceBackgroundMusic = audioSources[0];
        onButtonClick = audioSources[1];
        audioSourceBackgroundMusic.Play();

        Button[] buttons = FindObjectsOfType<Button>();

        // Выводим имена всех кнопок в консоль
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClickPlay);
        }
    }

    public void SettingsButton()
    {
        onButtonClick.Play();
        openSettingsMenu.SetActive(true);
        SettingsMenu = true;
    }

    public void BackToMenu()
    {
        openSettingsMenu.SetActive(false);
        SettingsMenu = false;
    }

    public void OnButtonClickPlay()
    {
        Debug.Log("Button clicked!");
        onButtonClick.Play();
    }


    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void StartButton()
    {
        ChangeScene();
    }
}