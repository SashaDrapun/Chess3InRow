﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GoToScene
{
    public class GoToShop : MonoBehaviour
    {
        public void GoToShopScene()
        {
            SceneManager.LoadScene(5);
        }
    }
}
