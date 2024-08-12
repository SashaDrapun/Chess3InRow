using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
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

        public static void SetPicture(Button button, string objectToSetPictureName)
        {
            button.GetComponent<Image>().sprite = GameObject.Find(objectToSetPictureName).GetComponent<Image>().sprite;
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

        private static GameObject FindHiddenObjectByName(string name)
        {
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }

            return null;
        }

        public static void FindHiddenObjectAndSetActive(string name)
        {
            FindHiddenObjectByName(name).SetActive(true);
        }

        public static void SetObjectNonActive(string name)
        {
            GameObject gameObject = GameObject.Find(name);
            gameObject.SetActive(false);
        }

        public static Button FindButton(string objectName)
        {
            Button[] allButtons = Resources.FindObjectsOfTypeAll<Button>();
            Button hiddenMusicButton = Array.Find(allButtons, btn => btn.name == objectName && !btn.gameObject.activeInHierarchy);

            if (hiddenMusicButton != null) return hiddenMusicButton;
            else return GameObject.Find(objectName).GetComponent<Button>();
        }

        public static void OutputInformation(string outputTextMeshProName, string outputInformation)
        {
            TextMeshProUGUI[] textMeshProObjects = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
            foreach (TextMeshProUGUI obj in textMeshProObjects)
            {
                if (obj.name == outputTextMeshProName)
                {
                    obj.text = outputInformation;
                    return;
                }
            }
        }
    }
}
