using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GeneralFunctionality
{
    public class AudioCode : MonoBehaviour
    {


        void Awake()
        {
            // Создаем объект AudioManager, если он еще не существует
            if (FindObjectsByType<AudioCode>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).Length > 1)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
