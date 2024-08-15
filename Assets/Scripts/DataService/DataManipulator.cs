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
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using UnityEngine;

    public class DataManipulator
    {
        private static readonly DataManipulator instance = new DataManipulator();

        private const string MapInformationFilePath = "/mapInformation.dat";
        private const string ShopInformationFilePath = "/shopInformation.dat";

        // Private constructor to prevent instantiation
        private DataManipulator() { }

        public static DataManipulator GetInstance()
        {
            return instance;
        }

        public static void SaveMapInformation(MapInformation mapInformation)
        {
            GetInstance().SaveData(MapInformationFilePath, mapInformation);
        }

        public static MapInformation LoadMapInformation()
        {
            return GetInstance().LoadData<MapInformation>(MapInformationFilePath);
        }

        public static void ResetData()
        {
            DataManipulator dataManipulator = GetInstance();
            dataManipulator.ResetFile(MapInformationFilePath);
            dataManipulator.ResetFile(ShopInformationFilePath);
        }

        public static void SaveShopInformation(ShopInformation shopInformation)
        {
            GetInstance().SaveData(ShopInformationFilePath, shopInformation);
        }

        public static ShopInformation LoadShopInformation()
        {
            return GetInstance().LoadData<ShopInformation>(ShopInformationFilePath);
        }

        private void SaveData<T>(string filePath, T data)
        {
            try
            {
                using FileStream file = File.Create(Application.persistentDataPath + filePath);
                BinaryFormatter bf = new();
                bf.Serialize(file, data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save data to {filePath}: {ex.Message}");
            }
        }

        private T LoadData<T>(string filePath)
        {
            if (File.Exists(Application.persistentDataPath + filePath))
            {
                try
                {
                    using FileStream file = File.Open(Application.persistentDataPath + filePath, FileMode.Open);
                    BinaryFormatter bf = new();
                    return (T)bf.Deserialize(file);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to load data from {filePath}: {ex.Message}");
                }
            }

            return Activator.CreateInstance<T>();
        }

        private void ResetFile(string filePath)
        {
            string fullPath = Application.persistentDataPath + filePath;
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                    Debug.Log($"Data reset complete for {filePath}!");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to delete data file {filePath}: {ex.Message}");
                }
            }
            else
            {
                Debug.LogError($"No save data to delete for {filePath}.");
            }
        }
    }
}
