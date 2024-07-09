using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayUI : MonoBehaviour
{
    [SerializeField] Button[] overlayTabButtons;
    [SerializeField] Button exitButton;

    [SerializeField] GameObject[] overlayWindows;

    private void Awake()
    {        
        for(int i = 0; i < overlayTabButtons.Length; i++)
        {
            int windowNumber = i;
            overlayTabButtons[i].onClick.AddListener(() => OverlayTab(windowNumber));
        }
    }

    private void Start()
    {
        overlayTabButtons[0].GetComponent<Image>().enabled = false;
        overlayWindows[0].SetActive(true);
    }

    public void OverlayTab(int windowNumber)
    {
        for(int i = 0; i < overlayWindows.Length; i++) 
        {
            if(i == windowNumber)
            {
                overlayTabButtons[i].GetComponent<Image>().enabled = false;
                overlayTabButtons[i].enabled = false;
                overlayWindows[i].SetActive(true);
            }
            else
            {
                overlayTabButtons[i].GetComponent<Image>().enabled = true;
                overlayTabButtons[i].enabled = true;
                overlayWindows[i].SetActive(false);
            }              
        }        
    }
}
