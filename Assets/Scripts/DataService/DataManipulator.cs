using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.DataService
{
    public class DataManipulator
    {
        public void SaveMapInformation(MapInformation mapInformation)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/mapInformation.dat");
            bf.Serialize(file, mapInformation);
            file.Close();
        }

        public MapInformation LoadMapInformation()
        {
            if (File.Exists(Application.persistentDataPath + "/mapInformation.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/mapInformation.dat", FileMode.Open);
                MapInformation mapInformation = (MapInformation)bf.Deserialize(file);
                file.Close();
                return mapInformation;
            }

            return new MapInformation();
        }

        public void ResetData()
        {
            if (File.Exists(Application.persistentDataPath + "/mapInformation.dat"))
            {
                File.Delete(Application.persistentDataPath + "/mapInformation.dat");
                
                Debug.Log("Data reset complete!");
            }
            else Debug.LogError("No save data to delete.");
        }

        public void SaveShopInformation(ShopInformation shopInformation)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/shopInformation.dat");
            bf.Serialize(file, shopInformation);
            file.Close();
        }

        public ShopInformation LoadShopInformation()
        {
            if (File.Exists(Application.persistentDataPath + "/shopInformation.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/shopInformation.dat", FileMode.Open);
                ShopInformation shopInformation = (ShopInformation)bf.Deserialize(file);
                file.Close();
                return shopInformation;
            }

            return new ShopInformation();
        }

    }
}
