using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuEvents : MonoBehaviour
{
    private AudioSource audioSourceBackgroundMusic;
    private AudioSource audioSourseOnButtonClick;

    private void Awake()
    {
        CheckPrefsForSettings();
        SetAudioSources();
        LoadSettings();
    }

    private void SetAudioSources()
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
        Button musicButton = FindButton("Music");
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
        Button soundsButton = FindButton("Sounds");

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

    private Button FindButton(string objectName)
    {
        Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();
        Button hiddenMusicButton = Array.Find(allButtons, btn => btn.name == objectName && !btn.gameObject.activeInHierarchy);

        if (hiddenMusicButton != null) return hiddenMusicButton;
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
}