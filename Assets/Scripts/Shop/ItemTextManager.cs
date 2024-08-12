using Assets.Scripts.LevelSettingsFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    public class ItemTextManager : MonoBehaviour
    {
        public ItemText[] itemTexts;

        public string GetItemText(int textIndex)
        {
            if (textIndex >= 0 && textIndex < itemTexts.Length)
            {
                return itemTexts[textIndex].text;

            }
            else
            {
                throw new MissingMemberException();
            }
        }
    }
}
