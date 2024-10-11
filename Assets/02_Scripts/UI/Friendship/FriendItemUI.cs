using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendItemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI friendName;
    [SerializeField] Image friendPortrait;
    [SerializeField] Sprite filledHeartSprite;
    [SerializeField] Sprite emptyHeartSprite;
    [SerializeField] List<Image> friendship;
    [SerializeField] List<Image> preferenceGift;
    AMFriendData friend;

    public void Setup(AMCharacterData _friend)
    {
        friend = _friend as AMFriendData;        
        friendName.text = friend.GetCharacterName();
        friendPortrait.sprite = friend.GetCharacterPortrait();

        for (int i = 0; i < friendship.Count; i++)
        {
            if (i < friend.GetFriendship())
            {
                friendship[i].sprite = filledHeartSprite;
            }
            else
            {
                friendship[i].sprite = emptyHeartSprite;
            }
        }


        int giftCount = 0;
        foreach (int itemID in friend.GetPreferenceItemID())
        {
            if(giftCount < friend.GetPreferenceItemID().Count)
            {
                preferenceGift[giftCount++].sprite = Managers.Data.GetItemData(itemID, false).itemSprite;
            }     
            else
            {
                break;
            }
        }
    }
}
