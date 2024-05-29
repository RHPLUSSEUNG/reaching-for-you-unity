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
        InvenPanel,
        ConsumeContent,
        EquipContent,
        CloseButton
    }

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
            childInfo.SetCharInfo(tempPlayer[i]);
        }
    }

    public void SetInventory()
    {
        int i = 0;
        foreach (KeyValuePair<GameObject, int> consume in Managers.Item.consumeInven)
        {
            Transform item = GetObject((int)repairUI.ConsumeContent).transform.GetChild(i);
            ConsumeRepairUI itemInfo = item.gameObject.GetComponent<ConsumeRepairUI>();
            // Image icon = consume.Key.sprite;
            // itemInfo.SetInfo(icon, consume.Value);
            // TODO : invenSlot 부족하면 Instantiate
        }
        int equipSize = Managers.Item.equipmentInven.Count;
        for (i = 0; i < equipSize; i++)
        {
            Transform equip = GetObject((int)repairUI.EquipContent).transform.GetChild(i);
            EquipRepairUI equipInfo = equip.gameObject.GetComponent<EquipRepairUI>();
            // equip.SetInfo();
        }
    }

    public void OnCloseButton(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(gameObject);
    }
}
