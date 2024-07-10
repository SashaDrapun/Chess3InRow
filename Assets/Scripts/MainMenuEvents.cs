using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;

    private Button _button;

    private List<Button> _menuButtons = new List<Button>();

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("StartGameButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();

        for(int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }


    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        
    }

    private void OnAllButtonClick(ClickEvent evt)
    {
        _audioSource.Play();
    }
}
