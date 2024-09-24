using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GiftListUI : MonoBehaviour
{
    [SerializeField] Transform giftContent;
    [SerializeField] GiftItemUI giftItemPrefab;
    DataList dataList;

    private void Start()
    {
        dataList = GameObject.Find("DataList").GetComponent<DataList>();
        Managers.Item.onUpdate += ReDraw;
        ReDraw();
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

            ConsumeData itemData = dataList.consumeList.Find(data => data.ItemID == itemID);

            if (itemData != null)
            {
                GiftItemUI giftItemInstance = Instantiate(giftItemPrefab, giftContent);
                giftItemInstance.SetUp(itemID, itemCount, itemData.itemSprite);
            }
        }
        
        foreach (int equipItemID in equipmentItems)
        {
            EquipmentData equipData = dataList.equipmentList.Find(data => data.ItemID == equipItemID);

            if (equipData != null)
            {
                GiftItemUI equipItemInstance = Instantiate(giftItemPrefab, giftContent);
                equipItemInstance.SetUp(equipItemID, 1, equipData.itemSprite);
            }
        }
    }
}

