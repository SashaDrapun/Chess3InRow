using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shop
{
    [CreateAssetMenu(fileName = "ItemText", menuName = "Shop/ItemText", order = 2)]
    public class ItemText : ScriptableObject
    {
        public string text;
    }
}
