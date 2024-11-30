using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideUI : MonoBehaviour
{
    [SerializeField] KeyCode toggleKey = KeyCode.Escape;
    [SerializeField] GameObject uiContainer = null;
    [SerializeField] Button exitButton;
    [SerializeField] CanvasController canvasController;

    private void Start()
    {
        uiContainer.SetActive(false);
        exitButton.onClick.AddListener(Toggle);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        canvasController.ChangeCanvasOrder();
        uiContainer.SetActive(!uiContainer.activeSelf);
    }
}
