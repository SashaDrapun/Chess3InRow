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
            CountShopItems = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                CountShopItems.Add(0);
            }
        }

        public int Money { get; set; }
        public List<int> CountShopItems;
    }

    
}
