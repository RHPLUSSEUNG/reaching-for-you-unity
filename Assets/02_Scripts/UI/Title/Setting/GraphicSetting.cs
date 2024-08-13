using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GraphicSetting : MonoBehaviour
{
    [SerializeField] TMP_Dropdown qualityDropdown;
    [SerializeField] TMP_Dropdown antiAliasingDropdown;
    [SerializeField] TMP_Text currentQualityText;
    [SerializeField] Slider brightnessSlider;
    float brightnessValue = 0.5f;

    private void Start()
    {
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
        InitializeGraphicsSettings();
    }

    public void InitializeGraphicsSettings()
    {        
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(OnQualityDropdownChanged);


        //[TODO:LSH] Anti Aliasing Setting
    }

    void OnBrightnessChanged(float value)
    {
        //[TODO:LSH] Difficult to work with URP
        brightnessValue = value;
        Screen.brightness = brightnessValue;
    }

    void OnQualityDropdownChanged(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }

}
