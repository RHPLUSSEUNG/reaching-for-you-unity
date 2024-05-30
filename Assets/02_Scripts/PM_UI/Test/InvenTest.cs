using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenTest : MonoBehaviour
{
    public GameObject Test;
    void Start()
    {
        PM_UI_Manager.InvenUI.SetPlayerInvenUI();
        for (int i = 0; i < Test.transform.childCount; i++)
        {
            if (Test.transform.GetChild(i).gameObject.GetComponent<Item>().type == ItemType.Consume && Managers.Item.consumeInven.ContainsKey(Test.transform.GetChild(i).gameObject))
            {
                Managers.Item.AddItem(Test.transform.GetChild(i).gameObject);
                Test.transform.GetChild(i).gameObject.GetComponent<ConsumeItemUI>().IncreaseCountUI();
                continue;
            }
            Managers.Item.AddItem(Test.transform.GetChild(i).gameObject);

            if (Test.transform.GetChild(i).gameObject.GetComponent<Item>().type == ItemType.Equipment)
            {
                EquipItemUI equipItem = PM_UI_Manager.UI.MakeSubItem<EquipItemUI>(PM_UI_Manager.InvenUI.invenContent.transform, "EquipItem");
                equipItem.invenItem = Test.transform.GetChild(i).gameObject;
                Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();       // 아이템 정보 가져오기
                equipItem.SetItemInfo(itemIcon);
                PM_UI_Manager.UI.HideUI(equipItem.gameObject);
            }
            else
            {
                ConsumeItemUI consumeItem = PM_UI_Manager.UI.MakeSubItem<ConsumeItemUI>(PM_UI_Manager.InvenUI.invenContent.transform, "ConsumeItem");
                consumeItem.invenItem = Test.transform.GetChild(i).gameObject;
                Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();
                consumeItem.SetItemInfo(itemIcon);
                PM_UI_Manager.UI.HideUI(consumeItem.gameObject);
            }
        }
    }
}
