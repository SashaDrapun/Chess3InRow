using Assets.Scripts.GeneralFunctionality;
using UnityEngine;
using UnityEngine.UI;

public class TogglePowerPanel : MonoBehaviour
{
    public GameObject panel; 
    private bool isExpanded = false; 

    void Start()
    {
        // Инициализация кнопки
        Button button = ObjectManager.FindButton("SuperPowerButton");
        button.onClick.AddListener(TogglePanelVisibility);

        // Изначально панель скрыта
        panel.SetActive(false);
    }

    void TogglePanelVisibility()
    {
        isExpanded = !isExpanded;
        panel.SetActive(isExpanded);
    }
}
