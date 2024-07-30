using Assets.Scripts.GeneralFunctionality;
using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuEvents : MonoBehaviour
{
    private void Awake()
    {
        CheckPrefsForSettings();
        LoadSettings();
    }

    private void LoadSettings()
    {
        SetAudioSources();
        LoadMusic(SettingsStatusService.GetCurrentMusicStatus());
        LoadSounds(SettingsStatusService.GetCurrentSoundsStatus());
    }

    private void SetAudioSources()
    {
        List<AudioSource> audioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
        AudioSourseListeners.AudioSourceBackgroundMusic = audioSources[1];
        AudioSourseListeners.AudioSourseOnButtonClick = audioSources[0];
    }

    private void LoadMusic(SettingsState settingsState)
    {
        Button musicButton = ObjectManager.FindButton("Music");
        AudioListenersService.ChangeAudioListenerBackgroundMusicStatus(settingsState);
        if (settingsState == SettingsState.On)
        {
            ObjectManager.SetPicture(musicButton, "MusicOn");
        }
        else
        {
            ObjectManager.SetPicture(musicButton, "MusicOff");
        }
    }

    private void LoadSounds(SettingsState settingsState)
    {
        Button soundsButton = ObjectManager.FindButton("Sounds");
        AudioListenersService.ChangeAudioListenerSoundsStatus(settingsState);
        if (settingsState == SettingsState.On)
        {
            ObjectManager.SetPicture(soundsButton, "SoundsOn");
        }
        else
        {
            ObjectManager.SetPicture(soundsButton, "SoundsOff");
        }
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