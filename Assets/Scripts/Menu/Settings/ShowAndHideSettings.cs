using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class ShowAndHideSettings : MonoBehaviour
    {
        public GameObject openSettingsMenu;

        public void OnSettingsButtonClick()
        {
            openSettingsMenu.SetActive(true);
        }

        public void OnBackToMenuClick()
        {
            openSettingsMenu.SetActive(false);
        }
    }
}
