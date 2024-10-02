using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class GiftListUI : MonoBehaviour
{
    [SerializeField] Transform giftContent;
    [SerializeField] GiftItemUI giftItemPrefab;
    [SerializeField] Transform alertPanel;
    Friend currentNpc;    

    private void Start()
    {        
        GiftUI.Instance.onGiftPanelUpdated += ReDraw;
        ReDraw();
        alertPanel.gameObject.SetActive(false);
    }

    private void ReDraw()
    {        
        foreach (Transform item in giftContent)
        {
            Destroy(item.gameObject);
        }
        
        Dictionary<int, int> consumeItems = Managers.Item.GetConsumeItem();        
        List<int> equipmentItems = Managers.Item.GetEquipmentItem();
        
        foreach (KeyValuePair<int, int> entry in consumeItems)
        {
            int itemID = entry.Key;
            int itemCount = entry.Value;

            ConsumeData itemData = Managers.Data.GetItemData(itemID, false) as ConsumeData;
            
            if (itemData != null)
            {
                GiftItemUI giftItemInstance = Instantiate(giftItemPrefab, giftContent);
                giftItemInstance.SetUp(itemID, itemCount, itemData.itemSprite);

                Button itemButton = giftItemInstance.GetComponentInChildren<Button>();
                itemButton.onClick.AddListener(() => ShowAlertPanel(itemData));
            }
        }
        
        foreach (int equipItemID in equipmentItems)
        {
            EquipmentData equipData = Managers.Data.GetItemData(equipItemID, false) as EquipmentData;

            if (equipData != null)
            {
                GiftItemUI giftItemInstance = Instantiate(giftItemPrefab, giftContent);
                giftItemInstance.SetUp(equipItemID, 1, equipData.itemSprite);

                Button itemButton = giftItemInstance.GetComponentInChildren<Button>();
                itemButton.onClick.AddListener(() => ShowAlertPanel(equipData));
            }
        }
    }

    public void SetNPC(Friend friendNPC)
    {
        currentNpc = friendNPC;
    }

    void ShowAlertPanel(ItemData itemData)
    {        
        alertPanel.gameObject.SetActive(true);
        alertPanel.GetComponent<GiftAlertUI>().SetUp(itemData.ItemID, currentNpc, itemData.ItemName);
    }
}

