using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LevelSettingsFolder
{
    public class LevelManager : MonoBehaviour
    {
        public LevelSettings[] levelSettings;

        public LevelSettings GetLevelSettings(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < levelSettings.Length)
            {
                return levelSettings[levelIndex];

            }
            else
            {
                Debug.LogWarning("Invalid level index");
            }

            return levelSettings[0];
        }
    }
}
