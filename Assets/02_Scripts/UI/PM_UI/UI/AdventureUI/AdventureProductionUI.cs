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
        EncounterSign
    }

    TextMeshProUGUI mapName;
    TextMeshProUGUI mapDescript;

    float waitOffset = 0.5f;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(ProductionUI));

        mapName = GetObject((int)ProductionUI.MapNameText).GetComponent<TextMeshProUGUI>();
        mapDescript = GetObject((int)ProductionUI.MapDescriptText).GetComponent<TextMeshProUGUI>();

        Managers.BattleUI.productionUI = GetComponent<AdventureProductionUI>();
        GetObject((int)ProductionUI.EncounterSign).SetActive(false);

        SetMapText();
        StartEnterFadeEffect();
    }

    // Test
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            EncounterProduction();
        }
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
        GetObject((int)ProductionUI.MapNameText).GetComponent<TextFadeEffect>().StartFadeEffect();
        GetObject((int)ProductionUI.MapDescriptText).GetComponent<TextFadeEffect>().StartFadeEffect();
    }

    public float EncounterProduction()
    {
        GetObject((int)ProductionUI.EncounterSign).SetActive(true);
        EncounerProduction production = GetObject((int)ProductionUI.EncounterSign).GetComponent<EncounerProduction>();
        StartCoroutine(production.Production());

        float waitTime = production.GetWaitTime() + waitOffset;

        return waitTime;
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
