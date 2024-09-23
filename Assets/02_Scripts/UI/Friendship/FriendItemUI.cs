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
    Friend friend;

    public void Setup(Friend _friend)
    {
        friend = _friend;
        friendName.text = friend.GetFriendName();
        friendPortrait.sprite = friend.GetFriendPortrait();

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
    }
}
