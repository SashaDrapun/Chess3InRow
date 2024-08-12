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
        private ShopItem selectedShopItem;

        public MainFunctionality()
        {
            selectedShopItem = ShopItem.None;
        }

        public void FreezeBonusClick()
        {
            selectedShopItem = ShopItem.Freezing;
            SetSellItemImage("Freeze");
        }

        public void AddBonusClick()
        {
            selectedShopItem = ShopItem.Adder;
            SetSellItemImage("AddMoves");
        }

        public void FuseBonusClick()
        {
            selectedShopItem = ShopItem.Fuse;
            SetSellItemImage("Boom");
        }

        public void RedistributorBonusClick()
        {
            selectedShopItem = ShopItem.Redistributor;
            SetSellItemImage("Randomize");
        }

        public void TeleporterBonusClick()
        {
            selectedShopItem = ShopItem.Teleporter;
            SetSellItemImage("Teleport");
        }

        private void SetSellItemImage(string imageFromName)
        {
            ObjectManager.SetPicture("SellItemImage", imageFromName);
        }
    }
}
