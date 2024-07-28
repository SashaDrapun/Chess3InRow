using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.BeforeLevelStart
{
    public class OnLoadScene : MonoBehaviour
    {
        private void OnEnable()
        {
            SetBackground();
            SetSceneElements();
        }

        private void SetBackground()
        {
            Image background = GameObject.Find($"Background").GetComponent<Image>();
            string imageToLoadName = "Before" + ApplicationData.SelectedLevel + "LevelStart";
            background.sprite = GameObject.Find(imageToLoadName).GetComponent<Image>().sprite;
        }

        private void SetSceneElements()
        {
            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] >= LevelStatus.OneStar)
            {
                ObjectManager.SetPicture("Star1", "StarOn");
            }

            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] >= LevelStatus.TwoStars)
            {
                ObjectManager.SetPicture("Star2", "StarOn");
            }

            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] >= LevelStatus.ThreeStars)
            {
                ObjectManager.SetPicture("Star3", "StarOn");
            }

            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] >= LevelStatus.SilverWings)
            {
                ObjectManager.SetPicture("MediumModeIndicator", "MediumModeOn"); 
            }

            if (ApplicationData.MapInformation.Levels[ApplicationData.CurrentLevel] >= LevelStatus.GoldenWings)
            {
                ObjectManager.SetPicture("MediumModeIndicator", "HardModeOn");
            }
        }
    }
}
