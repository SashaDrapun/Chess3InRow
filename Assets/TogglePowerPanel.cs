using Assets.Scripts.GeneralFunctionality;
using UnityEngine;
using UnityEngine.UI;

public class TogglePowerPanel : MonoBehaviour
{
    public GameObject panel; // Ссылка на панель, которая будет разворачиваться и сворачиваться
    private bool isExpanded = false; // Флаг для отслеживания состояния панели

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
