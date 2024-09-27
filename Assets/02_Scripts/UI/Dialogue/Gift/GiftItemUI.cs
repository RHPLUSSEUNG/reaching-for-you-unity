using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftItemUI : MonoBehaviour
{
    int itemID;
    [SerializeField] Image itemImage;
    [SerializeField] Button itemButton;
    [SerializeField] TextMeshProUGUI itemCountText;

    public void SetUp(int itemID, int itemCount, Sprite itemSprite)
    {
        this.itemID = itemID;

        itemImage.sprite = itemSprite;

        itemCountText.text = $"x{itemCount}";                
    }

}
