using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackToMapButton : MonoBehaviour
{
    public void OnBackToMapButtonClick()
    {
        SceneManager.LoadScene(1);
    }
}
