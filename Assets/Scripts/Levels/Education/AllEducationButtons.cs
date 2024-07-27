using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Map.Education
{
    public class AllEducationButtons : MonoBehaviour
    {

        private void OnEnable()
        {
            List<Button> allButtons = GetAllButtons();

            foreach (Button button in allButtons)
            {
                button.onClick.AddListener(OnAllButtonEducationClick);
            }
        }

        private List<Button> GetAllButtons()
        {
            Button[] hiddenButtons = Resources.FindObjectsOfTypeAll<Button>();
            Button[] visibleButtons = FindObjectsByType<Button>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return hiddenButtons.ToList().Concat(visibleButtons.ToList()).ToList();
        }

        public void OnAllButtonEducationClick()
        {
            GameObject button = EventSystem.current.currentSelectedGameObject;
            if (button.CompareTag("Locked")) return;
            ApplicationData.SelectedLevel = button.tag;
            SceneManager.LoadScene(2);
        }
    }
}
