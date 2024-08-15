using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Shop
{
    public class ShopSceneObjectManipulator
    {
        private static readonly ShopSceneObjectManipulator instance = new ShopSceneObjectManipulator();

        private ShopSceneObjectManipulator() { }

        public static ShopSceneObjectManipulator GetInstance()
        {
            return instance;
        }

        public static void SetScene()
        {
            ShopSceneObjectManipulator shopSceneObjectManipulator = GetInstance();
            shopSceneObjectManipulator.SetCountShopItems();
            shopSceneObjectManipulator.SetMoney();
        }

        private void SetCountShopItems()
        {
            for (int i = 0; i < ApplicationData.ShopInformation.CountShopItems.Count; i++)
            {
                string objectName = $"Count{(ShopItem)(i + 1)}";
                string outputCount = ApplicationData.ShopInformation.CountShopItems[i].ToString();

                ObjectManager.OutputInformation(objectName, outputCount);
            }
        }

        private void SetMoney()
        {
            ObjectManager.OutputInformation("CurrentMoney", ApplicationData.ShopInformation.Money.ToString());
        }
    }
}
