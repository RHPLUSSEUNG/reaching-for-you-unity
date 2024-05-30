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
            Image icon = equipItem.GetComponent<Image>();
            equipItem.SetInfo(icon);
        }
    }

    public void OnCloseButton(PointerEventData data)
    {
        PM_UI_Manager.UI.HideUI(gameObject);
    }
}
