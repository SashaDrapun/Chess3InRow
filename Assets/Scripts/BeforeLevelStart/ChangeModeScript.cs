using Assets.Scripts.GeneralFunctionality;
using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.BeforeLevelStart
{
    public class ChangeModeScript : MonoBehaviour
    {
        public void ChangeMode()
        {
            GameObject button = EventSystem.current.currentSelectedGameObject;

            if (button.CompareTag(LevelMode.Usual.ToString()))
            {
                ObjectManager.SetPicture("PickedMode", "LightModeOn");
                ApplicationData.CurrentLevelMode = LevelMode.Usual;
            }

            if (button.CompareTag(LevelMode.Silver.ToString()))
            {
                ObjectManager.SetPicture("PickedMode", "MediumModeOn");
                ApplicationData.CurrentLevelMode = LevelMode.Silver;
            }

            if (button.CompareTag(LevelMode.Gold.ToString()))
            {
                ObjectManager.SetPicture("PickedMode", "HardModeOn");
                ApplicationData.CurrentLevelMode = LevelMode.Gold;
            }
        }
    }
}
