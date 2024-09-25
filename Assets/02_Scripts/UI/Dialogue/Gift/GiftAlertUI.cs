using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class GiftAlertUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Button acceptButton;
    [SerializeField] Button refuseButton;
    [SerializeField] Button exitButton;
    Friend friend;
    int itemID;
    int friendshipLevel = 1;

    private void Start()
    {
        acceptButton.onClick.AddListener(SetAcceptButtonAction);
        refuseButton.onClick.AddListener(SetRefuseButtonAction);
        exitButton.onClick.AddListener(SetRefuseButtonAction);
    }

    public void SetUp(int _itemID, Friend friendNPC, string itemName)
    {
        friend = friendNPC;
        itemID = _itemID;
        descriptionText.text = $"{friend.GetFriendName()} 에게 {itemName} 주기";
    }

    public void SetAcceptButtonAction()
    {
        Managers.Item.RemoveItem(itemID);
        FriendshipManager.Instance.FriendshipHandler(friend, friendshipLevel);
        GiftUI.Instance.HideGiftPanel();
        this.transform.gameObject.SetActive(false);
    }

    public void SetRefuseButtonAction()
    {
        this.transform.gameObject.SetActive(false);
    }
}
