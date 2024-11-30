using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FriendsListUI : MonoBehaviour
{
    [SerializeField] Transform friendsContent;
    [SerializeField] FriendItemUI friendsInformationPrefab;
    [SerializeField] GameObject emptyText;
    void Start()
    {
        FriendshipManager.Instance.onUpdate += Redraw;
        Redraw();
    }

    public void Redraw()
    {
        foreach(Transform item in friendsContent)
        {
            Destroy(item.gameObject);
        }

        bool hasFriends = false;

        foreach (AMCharacterData friend in FriendshipManager.Instance.GetFriends())
        {
            FriendItemUI uiInstance = Instantiate<FriendItemUI>(friendsInformationPrefab, friendsContent);
            hasFriends = true;
            uiInstance.Setup(friend);
        }
        if (!hasFriends)
        {
            Instantiate(emptyText, friendsContent);
        }
    }

    void OnDestroy()
    {        
        if (FriendshipManager.Instance != null)
        {
            FriendshipManager.Instance.onUpdate -= Redraw;
        }
    }

}
