﻿using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Map.Education
{
    public class AllButtons : MonoBehaviour
    {

        private void Awake()
        {
            List<Button> allButtons = GetAllButtons();

            foreach (Button button in allButtons)
            {
                button.onClick.AddListener(OnAllButtonClick);
            }
        }

        private List<Button> GetAllButtons()
        {
            Button[] hiddenButtons = Resources.FindObjectsOfTypeAll<Button>();
            Button[] visibleButtons = FindObjectsOfType<Button>();
            return hiddenButtons.ToList().Concat(visibleButtons.ToList()).ToList();
        }

        public void OnAllButtonClick()
        {
            AudioSourseListeners.AudioSourseOnButtonClick.Play();
        }
    }
}
