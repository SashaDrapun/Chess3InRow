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
        public ItemTextManager ItemTextManager;

        private ShopItem selectedShopItem;

        public MainFunctionality()
        {
            selectedShopItem = ShopItem.None;
        }

        public void FreezeBonusClick()
        {
            selectedShopItem = ShopItem.Freezing;
            ObjectManager.OutputInformation("SellText", ItemTextManager.GetItemText((int)ShopItem.Freezing));
            SetSellItemImage("Freeze");
        }

        public void AddBonusClick()
        {
            selectedShopItem = ShopItem.Adder;
            ObjectManager.OutputInformation("SellText", ItemTextManager.GetItemText((int)ShopItem.Adder));
            SetSellItemImage("AddMoves");
        }

        public void FuseBonusClick()
        {
            selectedShopItem = ShopItem.Fuse;
            ObjectManager.OutputInformation("SellText", ItemTextManager.GetItemText((int)ShopItem.Fuse));
            SetSellItemImage("Boom");
        }

        public void RedistributorBonusClick()
        {
            selectedShopItem = ShopItem.Redistributor;
            ObjectManager.OutputInformation("SellText", ItemTextManager.GetItemText((int)ShopItem.Redistributor));
            SetSellItemImage("Randomize");
        }

        public void TeleporterBonusClick()
        {
            selectedShopItem = ShopItem.Teleporter;
            ObjectManager.OutputInformation("SellText", ItemTextManager.GetItemText((int)ShopItem.Teleporter));
            SetSellItemImage("Teleport");
        }

        private void SetSellItemImage(string imageFromName)
        {
            ObjectManager.SetPicture("SellItemImage", imageFromName);
        }
    }
}
