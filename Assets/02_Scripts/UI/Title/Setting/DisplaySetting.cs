using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySetting : MonoBehaviour
{    
    [SerializeField] Toggle fullScreenButton;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown frameRateDropdown;
    FullScreenMode screenMode;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionIndex = 0;
    int frameIndex = 0;

    private void Start()
    {
        InitializeScreen();
    }

    void InitializeScreen()
    {        
        resolutions.AddRange(Screen.resolutions);
        resolutionDropdown.options.Clear();

        int optionNum = 0;

        foreach(Resolution resolution in resolutions)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = resolution.width + "x" + resolution.height;
            resolutionDropdown.options.Add(optionData);

            if(resolution.width == Screen.width && resolution.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;                
            }
            optionNum++;
        }      
        
        resolutionDropdown.RefreshShownValue();

        if(Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow))
        {
            fullScreenButton.isOn = true;
        }
        else
        {
            fullScreenButton.isOn = false;
        }

        Application.targetFrameRate = 60;
    }

    public void FullScreenSelect(bool _isFullScreen)
    {
        if (_isFullScreen)
        {
            screenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            screenMode = FullScreenMode.Windowed;
        }

    }

    public void ChangeResolution(int _resolutionIndex)
    {
        resolutionIndex = _resolutionIndex;
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, screenMode);
    }
    
    public void ChangeFrameRate(int _frameIndex)
    {
        frameIndex = _frameIndex;
        switch(frameIndex)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 120;
                break;
            case 3:
                Application.targetFrameRate = 240;
                break;
            default:
                Debug.Log("non-existent fps");
                break;
        }
        
    }

}
