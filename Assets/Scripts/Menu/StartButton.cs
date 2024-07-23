using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class OnStartButton : MonoBehaviour
    {

        public void OnStartButtonClick()
        {
            SceneManager.LoadScene(1);
        }
    }
}
