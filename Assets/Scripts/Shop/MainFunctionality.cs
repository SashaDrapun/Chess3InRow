using Assets.Scripts.DataService;
using Assets.Scripts.GeneralFunctionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop
{
    public class MainFunctionality : MonoBehaviour
    {
        public ItemManager ItemTextManager;
        public Button freezeButton;
        public Button addButton;
        public Button fuseButton;
        public Button redistributorButton;
        public Button teleporterButton;

        private ShopItem selectedShopItem;

        private const string SellTextKey = "SellText";
        private const string SellItemImageKey = "SellItemImage";

        public MainFunctionality()
        {
            selectedShopItem = ShopItem.None;
        }

        private void Start()
        {
            freezeButton.onClick.AddListener(() => HandleBonusClick(ShopItem.Freezing, "Freeze"));
            addButton.onClick.AddListener(() => HandleBonusClick(ShopItem.Adder, "AddMoves"));
            fuseButton.onClick.AddListener(() => HandleBonusClick(ShopItem.Fuse, "Boom"));
            redistributorButton.onClick.AddListener(() => HandleBonusClick(ShopItem.Redistributor, "Randomize"));
            teleporterButton.onClick.AddListener(() => HandleBonusClick(ShopItem.Teleporter, "Teleport"));
        }

        public void HandleBonusClick(ShopItem shopItem, string imageName)
        {
            selectedShopItem = shopItem;
            OutputInformationAboutItem(shopItem);
            SetSellItemImage(imageName);
        }

        public void OutputInformationAboutItem(ShopItem shopItem)
        {
            string itemText = ItemTextManager.GetItemText(shopItem);
            int itemCost = ItemTextManager.GetItemCost(shopItem);
            ObjectManager.OutputInformation(SellTextKey, $"Стоимость товара: {itemCost}");
        }

        public void BuyButtonClick()
        {
            if (selectedShopItem == ShopItem.None) return;
            int itemCost = ItemTextManager.GetItemCost(selectedShopItem);

            if (IsEnoughMoneyToBuyItem(itemCost))
            {
                PerformPurchase(itemCost);
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

        private void PerformPurchase(int itemCost)
        {
            ApplicationData.ShopInformation.Money -= itemCost;
            ApplicationData.ShopInformation.CountShopItems[(int)selectedShopItem - 1]++;

            DataManipulator.SaveShopInformation(ApplicationData.ShopInformation);
            ShopSceneObjectManipulator.SetScene();
        }

        private void SetSellItemImage(string imageName)
        {
            ObjectManager.SetPicture(SellItemImageKey, imageName);
        }
    }
}
