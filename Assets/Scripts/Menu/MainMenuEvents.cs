using Assets.Scripts.Menu;
using System;
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
    private AudioSource audioSourseOnButtonClick;


    private void Awake()
    {
        CheckPrefsForSettings();
        SetAudiosources();
        LoadSettings();
    }

    private void SetAudiosources()
    {
        List<AudioSource> audioSources = GetComponents<AudioSource>().ToList();
        audioSourceBackgroundMusic = audioSources[0];
        audioSourseOnButtonClick = audioSources[1];
    }

    private void LoadSettings()
    {
        LoadMusic(SettingsStatusService.GetCurrentMusicStatus());
        LoadSounds(SettingsStatusService.GetCurrentSoundsStatus());
    }

    private void LoadMusic(SettingsState settingsState)
    {
        Button musicButton = FindMusicButton();
        if (settingsState == SettingsState.On)
        {
            audioSourceBackgroundMusic.Play();
            
            musicButton.GetComponent<Image>().sprite = GameObject.Find($"MusicOn").GetComponent<Image>().sprite;
        }
        else
        {
            audioSourceBackgroundMusic.Stop();
            musicButton.GetComponent<Image>().sprite = GameObject.Find($"MusicOff").GetComponent<Image>().sprite;
        }
    }

    private void LoadSounds(SettingsState settingsState)
    {
        Button soundsButton = FindSoundsButton();

        if (settingsState == SettingsState.On)
        {
            audioSourseOnButtonClick.mute = false;
            soundsButton.GetComponent<Image>().sprite = GameObject.Find($"SoundsOn").GetComponent<Image>().sprite;
        }
        else
        {
            audioSourseOnButtonClick.mute = true;
            soundsButton.GetComponent<Image>().sprite = GameObject.Find($"SoundsOff").GetComponent<Image>().sprite;
        }
    }

    private Button FindMusicButton()
    {
        Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();
        string objectName = "Music";

        Button hiddenMusicButton = Array.Find(allButtons, btn => btn.name == objectName && !btn.gameObject.activeInHierarchy);

        if (hiddenMusicButton != null) return hiddenMusicButton;
        else return GameObject.Find(objectName).GetComponent<Button>();
    }

    private Button FindSoundsButton()
    {
        Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();
        string objectName = "Sounds";

        Button hiddenSoundsButton = Array.Find(allButtons, btn => btn.name == objectName && !btn.gameObject.activeInHierarchy);

        if (hiddenSoundsButton != null) return hiddenSoundsButton;
        else return GameObject.Find(objectName).GetComponent<Button>();
    }

    private void CheckPrefsForSettings()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", (int)SettingsState.On);
        }

        if (!PlayerPrefs.HasKey("Sounds"))
        {
            PlayerPrefs.SetInt("Sounds", (int)SettingsState.On);
        }
    }

    public void SettingsButton()
    {
        audioSourseOnButtonClick.Play();
        openSettingsMenu.SetActive(true);
        SettingsMenu = true;
    }

    public void BackToMenu()
    {
        openSettingsMenu.SetActive(false);
        SettingsMenu = false;
    }

    public void OnAllButtonClickPlay()
    {
        audioSourseOnButtonClick.Play();
    }

    public void OnButtonMusicClick()
    {
        SettingsState musicStatus = SettingsStatusService.GetCurrentMusicStatus();
        SettingsState changedMusicStatus = SettingsStatusService.ChangeStatus(musicStatus);

        LoadMusic(changedMusicStatus);
        SettingsStatusService.SetCurrentMusicStatus(changedMusicStatus);
    }

    public void OnButtonSoundsClick()
    {
        SettingsState soundsStatus = SettingsStatusService.GetCurrentSoundsStatus();
        SettingsState changedSoundsStatus = SettingsStatusService.ChangeStatus(soundsStatus);

        LoadSounds(changedSoundsStatus);
        SettingsStatusService.SetCurrentSoundsStatus(changedSoundsStatus);
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