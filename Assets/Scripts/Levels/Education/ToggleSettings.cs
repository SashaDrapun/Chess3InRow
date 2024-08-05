using Assets.Scripts.GeneralFunctionality;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // ������ �� ������, ������� ����� ��������������� � �������������
    private bool isExpanded = false; // ���� ��� ������������ ��������� ������

    void Start()
    {
        // ������������� ������
        Button button = ObjectManager.FindButton("SettingsButton");
        button.onClick.AddListener(TogglePanelVisibility);

        // ���������� ������ ������
        panel.SetActive(false);
    }

    void TogglePanelVisibility()
    {
        isExpanded = !isExpanded;
        panel.SetActive(isExpanded);
    }
}