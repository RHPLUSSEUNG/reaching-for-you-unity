using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenTest : MonoBehaviour
{
    public GameObject Test;

    public Sprite[] test_sprite = new Sprite[3];
    void Awake ()
    {
        Debug.Log("아이템 생성");
        Managers.Item.AddItem(0);
        Managers.Item.AddItem(1);
        Managers.Item.AddItem(2);

        for(int i = 0; i < test_sprite.Length; i++)
        {
            Managers.InvenUI.test_sprite[i] = test_sprite[i];
        }
        Debug.Log("아이템 생성 완료");

        Managers.InvenUI.SetInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(Managers.InvenUI.inven_state == false)
            {
                Managers.InvenUI.inven_state = true;
                Managers.UI.ShowUI(Managers.InvenUI.invenUI);
            }
            else
            {
                Managers.InvenUI.inven_state = false;
                Managers.UI.HideUI(Managers.InvenUI.invenUI);
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Managers.Item.AddItem(0);
            Managers.InvenUI.TempInvenUI(0);
        }
    }

    private void TempUpdate()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            foreach(KeyValuePair<int, int> consume in Managers.Item.consumeInven)
            {
                Debug.Log($"consumeInven Count : {Managers.Item.consumeInven.Count}");
                Debug.Log($"consume : {consume.Key}, {consume.Value}");
            }
            Managers.InvenUI.player = gameObject;
            Equip_Item equipInfo = Managers.InvenUI.player.GetComponent<Equip_Item>();
            Managers.InvenUI.SetPlayerEquipUI(equipInfo);
            for (int i = 0; i < Test.transform.childCount; i++)
            {
                // Managers.Item.AddItem(Test.transform.GetChild(i).gameObject);

                if (Test.transform.GetChild(i).gameObject.GetComponent<Item>().type == ItemType.Equipment)
                {
                    EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(Managers.InvenUI.invenContent.transform, "EquipItem");
                    equipItem.invenItem = Test.transform.GetChild(i).gameObject;
                    Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();       // 아이템 정보 가져오기
                    equipItem.SetItemInfo();
                    Managers.UI.HideUI(equipItem.gameObject);
                }
                else
                {
                    ConsumeItemUI consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
                    consumeItem.invenItem = Test.transform.GetChild(i).gameObject;
                    Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();
                    consumeItem.SetItemInfo(itemIcon.sprite);
                    Managers.UI.HideUI(consumeItem.gameObject);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject portion = Util.FindChild(Test, "Atk_Portion_Big");
            Debug.Log(portion);
            if(portion.GetComponent<Item>().type == ItemType.Consume /* &&*/ /*Managers.Item.consumeInven.ContainsKey(portion)*/)
            {
                // Managers.Item.AddItem(portion);
                for(int i = 0; i < Managers.InvenUI.invenContent.transform.childCount; i++)
                {
                    GameObject invenItem = Managers.InvenUI.invenContent.transform.GetChild(i).GetComponent<InvenItemUI>().invenItem;
                    if (invenItem == portion)
                    {
                        Managers.InvenUI.invenContent.transform.GetChild(i).GetComponent<ConsumeItemUI>().IncreaseCountUI();
                    }
                }
            }
        }
    }
}
