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
    }
}
