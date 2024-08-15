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

        public string GetItemText(int textIndex)
        {
            if (textIndex >= 0 && textIndex < itemInfo.Length)
            {
                return itemInfo[textIndex].text;

            }
            else
            {
                throw new MissingMemberException();
            }
        }

        public int GetItemCost(int itemIndex)
        {
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
