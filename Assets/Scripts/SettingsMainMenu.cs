using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using ButtonUIElements = UnityEngine.UIElements.Button;

public class SettingsMainMenu : MonoBehaviour
{

    public static bool SettingsMenu;
    public GameObject openSettingsMenu;
    private ButtonUIElements startButton;
    private UIDocument document;

    private void Awake()
    {
        document = GetComponent<UIDocument>();

        startButton = document.rootVisualElement.Q("StartGameButton") as ButtonUIElements;
    }
    public void SettingsButton()
    {
        openSettingsMenu.SetActive(true);
        SettingsMenu = true;
        startButton.visible = false;
    }

    public void BackToMenu()
    {
        openSettingsMenu.SetActive(false);
        SettingsMenu = false;
        startButton.visible = true;
    }
}
