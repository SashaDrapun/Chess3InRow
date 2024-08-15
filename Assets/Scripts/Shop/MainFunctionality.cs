using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class MainFunctionality : MonoBehaviour
    {
        public ItemManager ItemTextManager;

        private ShopItem selectedShopItem;

        public MainFunctionality()
        {
            selectedShopItem = ShopItem.None;
        }

        public void FreezeBonusClick()
        {
            selectedShopItem = ShopItem.Freezing;
            OutputInformationAboutItem(ShopItem.Freezing);
            SetSellItemImage("Freeze");
        }

        public void AddBonusClick()
        {
            selectedShopItem = ShopItem.Adder;
            OutputInformationAboutItem(ShopItem.Adder);
            SetSellItemImage("AddMoves");
        }

        public void FuseBonusClick()
        {
            selectedShopItem = ShopItem.Fuse;
            OutputInformationAboutItem(ShopItem.Fuse);
            SetSellItemImage("Boom");
        }

        public void RedistributorBonusClick()
        {
            selectedShopItem = ShopItem.Redistributor;
            OutputInformationAboutItem(ShopItem.Redistributor); 
            SetSellItemImage("Randomize");
        }

        public void TeleporterBonusClick()
        {
            selectedShopItem = ShopItem.Teleporter;
            OutputInformationAboutItem(ShopItem.Teleporter);
            SetSellItemImage("Teleport");
        }

        public void OutputInformationAboutItem(ShopItem shopItem)
        {
            string itemText = ItemTextManager.GetItemText((int)shopItem - 1);
            int itemCost = ItemTextManager.GetItemCost((int)shopItem - 1);
            ObjectManager.OutputInformation("SellText", $"Cтоимость товара: {itemCost}");
        }

        public void BuyButtonClick()
        {
            if (selectedShopItem == ShopItem.None) return;
            int itemCost = ItemTextManager.GetItemCost((int)selectedShopItem - 1);

            if (IsEnoughMoneyToBuyItem(itemCost))
            {
                ApplicationData.ShopInformation.Money -= itemCost;
                ApplicationData.ShopInformation.CountShopItems[(int)selectedShopItem - 1]++;

                DataManipulator dataManipulator = new();
                dataManipulator.SaveShopInformation(ApplicationData.ShopInformation);

                ShopSceneObjectManipulator.SetScene();
            }
            else
            {
                // Вывести панель с предложением посмотреть рекламу, или купить монеты
            }
        }

        private bool IsEnoughMoneyToBuyItem(int itemCost)
        {
            return ApplicationData.ShopInformation.Money >= itemCost;
        }

        private void SetSellItemImage(string imageFromName)
        {
            ObjectManager.SetPicture("SellItemImage", imageFromName);
        }
    }
}
