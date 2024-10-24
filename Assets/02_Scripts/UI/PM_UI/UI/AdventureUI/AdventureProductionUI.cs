using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AdventureProductionUI : UI_Popup
{
    enum ProductionUI
    {
        EnterTextPanel,
        MapNameText,
        MapDescriptText,
        EncounterPanel
    }

    TextMeshProUGUI mapName;
    TextMeshProUGUI mapDescript;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(ProductionUI));

        mapName = GetObject((int)ProductionUI.MapNameText).GetComponent<TextMeshProUGUI>();
        mapDescript = GetObject((int)ProductionUI.MapDescriptText).GetComponent<TextMeshProUGUI>();

        Managers.BattleUI.productionUI = GetComponent<AdventureProductionUI>();
        GetObject((int)ProductionUI.EncounterPanel).SetActive(false);

        SetMapText();
        StartEnterFadeEffect();
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

    public void StartEnterFadeEffect()
    {
        GetObject((int)ProductionUI.EnterTextPanel).GetComponent<ImageFadeEffect>().StartFadeEffect();
        GetObject((int)ProductionUI.MapNameText).GetComponent<TextFadeEffect>().StartFadeEffect();
        GetObject((int)ProductionUI.MapDescriptText).GetComponent<TextFadeEffect>().StartFadeEffect();
    }

    public void EncounterProduction()
    {
        GetObject((int)ProductionUI.EncounterPanel).SetActive(true);
        EncounterProduction production = GetObject((int)ProductionUI.EncounterPanel).GetComponent<EncounterProduction>();
        StartCoroutine(production.Encounter());
    }

    void SettingDesertUI()
    {
        string name = "�縷 ����";
        string descript = "Ȳ���� �縷�� ���� �����̴�.\n���Ϳ� ��������!";
        Color textColor = new Color(251 / 255f, 148 / 255f, 68 / 255f, 255 / 255f);
        mapName.text = name;
        mapDescript.text = descript;
        mapName.color = textColor;
        mapDescript.color = textColor;
    }

    void SettingAquaUI()
    {
        string name = "�ٴ� ����";
        string descript = "�ٴٿ� ��� ��� �����̴�.\n�ͻ����� �ʵ��� ��������!";
        Color textColor = new Color(67 / 255f, 165 / 255f, 250 / 255f, 255 / 255f);
        mapName.text = name;
        mapDescript.text = descript;
        mapName.color = textColor;
        mapDescript.color = textColor;
    }
}
