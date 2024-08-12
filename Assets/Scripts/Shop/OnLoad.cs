﻿using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class OnLoad : MonoBehaviour
    {
        private void Start()
        {
            DataManipulator dataManipulator = new();
            ApplicationData.ShopInformation = dataManipulator.LoadShopInformation();
            ObjectManager.OutputInformation("CurrentMoney", ApplicationData.ShopInformation.Money.ToString());
        }
    }
}
