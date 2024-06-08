using UnityEngine;
using UnityEngine.UI;

public class InvenTest : MonoBehaviour
{
    public GameObject Test;
    void Start()
    {
       
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InvenUI invenUI = Managers.UI.CreatePopupUI<InvenUI>("InvenUI");
            Managers.InvenUI.player = gameObject;
            Equip_Item equipInfo = Managers.InvenUI.player.GetComponent<Equip_Item>();
            Managers.InvenUI.SetPlayerEquipUI(equipInfo);
            for (int i = 0; i < Test.transform.childCount; i++)
            {
                Managers.Item.AddItem(Test.transform.GetChild(i).gameObject);

                if (Test.transform.GetChild(i).gameObject.GetComponent<Item>().type == ItemType.Equipment)
                {
                    EquipItemUI equipItem = Managers.UI.MakeSubItem<EquipItemUI>(Managers.InvenUI.invenContent.transform, "EquipItem");
                    equipItem.invenItem = Test.transform.GetChild(i).gameObject;
                    Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();       // 아이템 정보 가져오기
                    equipItem.SetItemInfo(itemIcon);
                    Managers.UI.HideUI(equipItem.gameObject);
                }
                else
                {
                    ConsumeItemUI consumeItem = Managers.UI.MakeSubItem<ConsumeItemUI>(Managers.InvenUI.invenContent.transform, "ConsumeItem");
                    consumeItem.invenItem = Test.transform.GetChild(i).gameObject;
                    Image itemIcon = Test.transform.GetChild(i).gameObject.GetComponent<Image>();
                    consumeItem.SetItemInfo(itemIcon);
                    Managers.UI.HideUI(consumeItem.gameObject);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            GameObject portion = Util.FindChild(Test, "Atk_Portion_Big");
            Debug.Log(portion);
            if(portion.GetComponent<Item>().type == ItemType.Consume && Managers.Item.consumeInven.ContainsKey(portion))
            {
                Managers.Item.AddItem(portion);
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
