using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySetting : MonoBehaviour
{
    [SerializeField] Toggle fullScreenToggle;
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

        foreach (Resolution resolution in resolutions)
        {
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
            optionData.text = resolution.width + "x" + resolution.height;
            resolutionDropdown.options.Add(optionData);

            if (resolution.width == Screen.width && resolution.height == Screen.height)
            {
                resolutionDropdown.value = optionNum;
            }
            optionNum++;
        }

        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;

        Application.targetFrameRate = 60;
    }

    public void OnFullScreenToggleChanged(bool isFullScreen)
    {
        screenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, screenMode);
    }

    public void OnResolutionDropdownChanged(int resolutionIndex)
    {
        this.resolutionIndex = resolutionIndex;
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, screenMode);
    }

    public void OnFrameRateDropdownChanged(int frameIndex)
    {
        this.frameIndex = frameIndex;
        switch (frameIndex)
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
                Debug.Log("Non-existent fps");
                break;
        }
    }
}
