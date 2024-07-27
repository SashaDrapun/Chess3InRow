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
            bool ggg = File.Exists(Application.persistentDataPath + "/mapInformation.dat");
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

    }
}
