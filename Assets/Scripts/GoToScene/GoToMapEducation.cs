using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Menu
{
    public class GoToEducationMap : MonoBehaviour
    {
        public void GoToMapEducation()
        {
            SceneManager.LoadScene(1);
        }
    }
}
