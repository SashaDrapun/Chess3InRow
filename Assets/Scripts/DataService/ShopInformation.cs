using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataService
{
    [Serializable]
    public class ShopInformation
    {
        public ShopInformation() 
        {
            ShopItems = new List<ShopItem>();
        }

        public int Money { get; set; }
        public List<ShopItem> ShopItems;
    }

    
}
