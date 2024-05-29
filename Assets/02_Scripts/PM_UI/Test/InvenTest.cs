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
        for(int i = 0; i < Test.transform.childCount; i++)
        {
            Managers.Item.AddItem(Test.transform.GetChild(i).gameObject);
            InvenItemUI invenItem = PM_UI_Manager.UI.MakeSubItem<InvenItemUI>(PM_UI_Manager.InvenUI.invenContent.transform, "InvenItem");
            invenItem.invenItem = Test.transform.GetChild(i).gameObject;
            Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();
            invenItem.SetItemInfo(itemIcon);
            PM_UI_Manager.UI.HideUI(invenItem.gameObject);
        }
    }

    void Update()
    {
        
    }
}
