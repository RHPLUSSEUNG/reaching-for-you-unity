using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Repair : UI_Popup
{
    [SerializeField] TestPlayerInfo[] tempPlayer = new TestPlayerInfo[4];       // test
    enum repairUI
    {
        CharList,
        InvenPanel,
        CloseButton
    }

    GameObject charList;
    GameObject invenPanel;
    public override void Init()
    {
        Bind<GameObject>(typeof(repairUI));

        charList = Get<GameObject>((int)repairUI.CharList);
        GameObject close = Get<GameObject>((int)repairUI.CloseButton);
        BindEvent(close, OnCloseButton, Define.UIEvent.Click);

        SetInventory();
        SetPlayerInfo();
        gameObject.SetActive(false);
    }

    void SetPlayerInfo()
    {
        GameObject child;
        for(int i = 0; i < tempPlayer.Length; i++)
        {
            child = charList.transform.GetChild(i).gameObject;
            UI_CharInfo childInfo = child.GetComponent<UI_CharInfo>();
            childInfo.SetCharInfo(tempPlayer[i]);
        }
        gameObject.SetActive(false);
    }

    void SetInventory()
    {
        invenPanel = Get<GameObject>((int)repairUI.InvenPanel);
        GameObject itemInven = Util.FindChild(invenPanel, "ItemInven", true);
        GameObject itemContent = Util.FindChild(itemInven, "Content", true);
        GameObject equipInven = Util.FindChild(invenPanel, "EquipInven", true);
        GameObject equipContent = Util.FindChild(equipInven, "Content", true);
        

        int i = 0;
        foreach (KeyValuePair<GameObject, int> consume in Managers.Item.consumeInven)
        {
            Transform item = itemContent.transform.GetChild(i);
            UI_ConsumeItem itemInfo = item.gameObject.GetComponent<UI_ConsumeItem>();
            // Image icon = consume.Key.sprite;
            // itemInfo.SetInfo(icon, consume.Value);
            // TODO : invenSlot 부족하면 Instantiate
        }
        int equipSize = Managers.Item.equipmentInven.Count;
        for (i = 0; i < equipSize; i++)
        {
            Transform equip = equipContent.transform.GetChild(i);
            UI_EquipItem equipInfo = equip.gameObject.GetComponent<UI_EquipItem>();
            // equip.SetInfo();
        }
    }

    public void OnCloseButton(PointerEventData data)
    {
        gameObject.SetActive(false);
    }
}
