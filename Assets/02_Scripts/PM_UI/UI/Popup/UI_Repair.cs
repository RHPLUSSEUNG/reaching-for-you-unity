using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Repair : UI_Popup
{
    public TestPlayerInfo[] tempPlayer = new TestPlayerInfo[4];       // test
    enum repairUI
    {
        CharList,
        Head,
        Body,
        Weapon,
        EquipItem,
        HPBar,
        MPBar,
        ConsumeContent,
        EquipContent,
        CloseButton
    }

    public Equip_Item focusEquip;
    public TestPlayerInfo focusStat;
    GameObject charList;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(repairUI));

        charList = Get<GameObject>((int)repairUI.CharList);
        GameObject close = Get<GameObject>((int)repairUI.CloseButton);
        BindEvent(close, OnCloseButton, Define.UIEvent.Click);
    }

    public void SetPlayerInfo()
    {
        GameObject child;
        for(int i = 0; i < tempPlayer.Length; i++)
        {
            child = charList.transform.GetChild(i).gameObject;
            UI_CharInfo childInfo = child.GetComponent<UI_CharInfo>();
            Equip_Item equipInfo = tempPlayer[i].gameObject.GetComponent<Equip_Item>();
            childInfo.SetCharInfo(tempPlayer[i], equipInfo);
        }
        UpdatePlayerInfo(tempPlayer[0], tempPlayer[0].gameObject.GetComponent<Equip_Item>());
    }

    public void SetInventory()
    {
        GameObject consumeContent = GetObject((int)repairUI.ConsumeContent);
        GameObject equipContent = GetObject((int)repairUI.EquipContent);
        int i = 0;
        foreach (KeyValuePair<GameObject, int> consume in Managers.Item.consumeInven)
        {
            ConsumeRepairUI consumeItem = PM_UI_Manager.UI.MakeSubItem<ConsumeRepairUI>(consumeContent.transform, "ConsumeRepairUI");
            Image icon = consume.Key.GetComponent<Image>();
            consumeItem.SetInfo(icon, consume.Value);
        }
        int equipSize = Managers.Item.equipmentInven.Count;
        for (i = 0; i < equipSize; i++)
        {
            EquipRepairUI equipItem = PM_UI_Manager.UI.MakeSubItem<EquipRepairUI>(equipContent.transform, "EquipRepairUI");
            Image icon = Managers.Item.equipmentInven[i].GetComponent<Image>();
            equipItem.SetInfo(icon);
        }
    }

    public void UpdatePlayerInfo(TestPlayerInfo statInfo, Equip_Item equipInfo)
    {
        focusStat = statInfo;
        focusEquip = equipInfo;

        GetObject((int)repairUI.Head).GetComponent<Image>().sprite = equipInfo.Head.GetComponent<Image>().sprite;
        GetObject((int)repairUI.Body).GetComponent<Image>().sprite = equipInfo.Body.GetComponent<Image>().sprite;
        GetObject((int)repairUI.Weapon).GetComponent<Image>().sprite = equipInfo.Weapon.GetComponent<Image>().sprite;

        BarUI hpBar = GetObject((int)repairUI.HPBar).GetComponent<BarUI>();
        BarUI mpBar = GetObject((int)repairUI.MPBar).GetComponent<BarUI>();
        hpBar.SetPlayerStat(statInfo.hp, statInfo.maxHp);
        mpBar.SetPlayerStat(statInfo.mp, statInfo.maxMp);

        for (int i = 0; i < equipInfo.Consumes.Length; i++)
        {
            // TODO : ÀåÂø ¼ÒºñÅÛ UI
        }
    }

    public void OnCloseButton(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(gameObject);
    }
}
