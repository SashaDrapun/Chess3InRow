using Assets.Scripts.ChessFigures;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.BeforeLevelStart
{
    public class StartLevelButton : MonoBehaviour
    {
        private void OnEnable()
        {
            GameObject.Find($"StartLevelButton").GetComponent<Button>().onClick.AddListener(OnStartLevelButtonClick);
        }

        public void OnStartLevelButtonClick()
        {
            SceneManager.LoadScene(3);
        }
    }
}
