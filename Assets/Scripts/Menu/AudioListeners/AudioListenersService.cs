using Assets.Scripts.Menu.AudioListeners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public static class AudioListenersService
    {
        public static void ChangeAudioListenerBackgroundMusicStatus(SettingsState settingsState)
        {
            if (settingsState == SettingsState.On)
            {
                AudioSourseListeners.AudioSourceBackgroundMusic.Play();
            }
            else
            {
                AudioSourseListeners.AudioSourceBackgroundMusic.Stop();
            }
        }

        public static void ChangeAudioListenerSoundsStatus(SettingsState settingsState)
        {
            if (settingsState == SettingsState.On)
            {
                AudioSourseListeners.AudioSourseOnButtonClick.mute = false;
            }
            else
            {
                AudioSourseListeners.AudioSourseOnButtonClick.mute = true;
            }
        }
    }
}
