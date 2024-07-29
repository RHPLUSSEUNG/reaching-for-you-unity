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
    [SerializeField] TMP_Text currentQualityText;
    int textureQuality;
    int antiAliasing;
    int vSync;
    [SerializeField] Slider brightnessSlider;
    float brightnessValue = 0.5f;

    private void Start()
    {
        brightnessSlider.onValueChanged.AddListener(BrightnessChanged);
    }

    public void InitializeGraphic()
    {
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        //currentQualityText.text = $"Current Quality : {QualitySettings.names[QualitySettings.GetQualityLevel()]}";

        // 드롭다운의 값이 변경될 때마다 ChangeQuality 함수를 호출합니다.
        qualityDropdown.onValueChanged.AddListener(ChangeQuality);
    }

    void BrightnessChanged(float value)
    {
        brightnessValue = value;
        print(brightnessValue);
        Screen.brightness = brightnessValue;
    }

    void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
        //currentQualityText.text = $"Current Quality : {QualitySettings.names[index]}";
    }
    
}
