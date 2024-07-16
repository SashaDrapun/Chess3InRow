using UnityEngine;

namespace Assets.Scripts.Menu
{
    public static class SettingsStatusService
    {
        public static SettingsState ChangeStatus(SettingsState settingsState)
        {
            if (settingsState == SettingsState.On) return SettingsState.Off;
            else return SettingsState.On;
        }

        public static SettingsState GetCurrentMusicStatus()
        {
            return (SettingsState)PlayerPrefs.GetInt("Music");
        }

        public static SettingsState GetCurrentSoundsStatus()
        {
            return (SettingsState)PlayerPrefs.GetInt("Sounds");
        }

        public static void SetCurrentMusicStatus(SettingsState settingsState)
        {
            PlayerPrefs.SetInt("Music", (int)settingsState);
        }

        public static void SetCurrentSoundsStatus(SettingsState settingsState)
        {
            PlayerPrefs.SetInt("Sounds", (int)settingsState);
        }
    }
}
