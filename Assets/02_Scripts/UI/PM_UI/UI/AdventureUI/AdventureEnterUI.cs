using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AdventureEnterUI : UI_Popup
{
    enum mapEnterUI
    {
        TextPanel,
        MapNameText,
        MapDescriptText
    }

    TextMeshProUGUI mapName;
    TextMeshProUGUI mapDescript;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(mapEnterUI));

        mapName = GetObject((int)mapEnterUI.MapNameText).GetComponent<TextMeshProUGUI>();
        mapDescript = GetObject((int)mapEnterUI.MapDescriptText).GetComponent<TextMeshProUGUI>();

        SetMapText();
        StartFadeEffect();
    }

    public void SetMapText()
    {
        switch(AdventureManager.StageNumber)
        {
            case 0:
                SettingDesertUI();
                break;
            case 1:
                SettingAquaUI();
                break;
        }
    }

    public void StartFadeEffect()
    {
        GetObject((int)mapEnterUI.TextPanel).GetComponent<ImageFadeEffect>().StartFadeEffect();
        GetObject((int)mapEnterUI.MapNameText).GetComponent<TextFadeEffect>().StartFadeEffect();
        GetObject((int)mapEnterUI.MapDescriptText).GetComponent<TextFadeEffect>().StartFadeEffect();
    }

    void SettingDesertUI()
    {
        string name = "사막 유적";
        string descript = "황량한 사막의 신전 내부이다.\n몬스터에 주의하자!";
        Color textColor = new Color(251 / 255f, 148 / 255f, 68 / 255f, 255 / 255f);
        mapName.text = name;
        mapDescript.text = descript;
        mapName.color = textColor;
        mapDescript.color = textColor;
    }

    void SettingAquaUI()
    {
        string name = "바다 신전";
        string descript = "바다에 잠긴 고대 신전이다.\n익사하지 않도록 주의하자!";
        Color textColor = new Color(67 / 255f, 165 / 255f, 250 / 255f, 255 / 255f);
        mapName.text = name;
        mapDescript.text = descript;
        mapName.color = textColor;
        mapDescript.color = textColor;
    }
}
