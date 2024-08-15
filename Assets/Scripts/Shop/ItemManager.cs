using Assets.Scripts.DataService;
using Assets.Scripts.LevelSettingsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class ItemManager : MonoBehaviour
    {
        public ItemInfo[] itemInfo;

        public string GetItemText(ShopItem shopItem)
        {
            int itemIndex = (int)shopItem - 1;
            if (itemIndex >= 0 && itemIndex < itemInfo.Length)
            {
                return itemInfo[itemIndex].text;

            }
            else
            {
                throw new MissingMemberException();
            }
        }

        public int GetItemCost(ShopItem shopItem)
        {
            int itemIndex = (int)shopItem - 1;
            if (itemIndex >= 0 && itemIndex < itemInfo.Length)
            {
                return itemInfo[itemIndex].cost;

            }
            else
            {
                throw new MissingMemberException();
            }
        }
    }
}
