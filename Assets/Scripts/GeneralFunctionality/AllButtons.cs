﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    
    public class AllButtons : MonoBehaviour
    {
        private void OnEnable()
        {
            List<Button> allButtons = GetAllButtons();

            foreach (Button button in allButtons)
            {
                button.onClick.AddListener(OnAllButtonClick);
            }
        }

        private List<Button> GetAllButtons()
        {
            return FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }

        public void OnAllButtonClick()
        {
            AudioSourseListeners.AudioSourseOnButtonClick.Play();
        }
    }
}
