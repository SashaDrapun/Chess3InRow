using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GeneralFunctionality
{
    public static class ObjectManager
    {
        public static void SetPicture(string objectToSetPictureName, string objectFromSetPictureName)
        {
            Image objectToSetPicture = GameObject.Find(objectToSetPictureName).GetComponent<Image>();
            Image objectFromSetPicture = GameObject.Find(objectFromSetPictureName).GetComponent<Image>();

            objectToSetPicture.sprite = objectFromSetPicture.sprite;
        }

        public static void SetPicture(string objectToSetPictureName, Sprite sprite)
        {
            Image objectToSetPicture = GameObject.Find(objectToSetPictureName).GetComponent<Image>();
            objectToSetPicture.sprite = sprite;
        }

        public static void SetTag(string objectToSetTagName, string tag)
        {
            GameObject objectToSetTag = GameObject.Find(objectToSetTagName);
            objectToSetTag.tag = tag;
        }

        public static Sprite GetSprite(string imageName)
        {
            return GameObject.Find(imageName).GetComponent<Image>().sprite;
        }
    }
}
