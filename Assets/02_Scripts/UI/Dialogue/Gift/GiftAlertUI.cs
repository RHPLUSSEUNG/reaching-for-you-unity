using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftAlertUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Button acceptButton;
    [SerializeField] Button refuseButton;
    [SerializeField] Button exitButton;
    AMCharacterData friend;
    int itemID;
    int friendshipLevel = 1;

    private void Start()
    {
        acceptButton.onClick.AddListener(SetAcceptButtonAction);
        refuseButton.onClick.AddListener(SetRefuseButtonAction);
        exitButton.onClick.AddListener(SetRefuseButtonAction);
    }

    public void SetUp(int _itemID, AMCharacterData friendNPC, string itemName)
    {
        friend = friendNPC;
        itemID = _itemID;
        descriptionText.text = $"{friend.GetCharacterName()} ���� {itemName} �ֱ�";
    }

    public void SetAcceptButtonAction()
    {
        Managers.Item.RemoveItem(itemID, 1);
        FriendshipManager.Instance.FriendshipHandler(friend, friendshipLevel);
        GiftUI.Instance.HideGiftPanel();        
        this.transform.gameObject.SetActive(false);
    }

    public void SetRefuseButtonAction()
    {
        this.transform.gameObject.SetActive(false);
    }
}
