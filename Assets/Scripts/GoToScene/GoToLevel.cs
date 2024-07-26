using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GoToScene
{
    public class GoToLevel : MonoBehaviour
    {
        public void GoToLevelScene()
        {
            SceneManager.LoadScene(3);
        }
    }
}
