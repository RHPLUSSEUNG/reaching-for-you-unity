using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FriendsListUI : MonoBehaviour
{
    [SerializeField] Transform friendsContent;
    [SerializeField] FriendItemUI friendsInformationPrefab;
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

        foreach (Friend friend in FriendshipManager.Instance.GetFriends())
        {
            FriendItemUI uiInstance = Instantiate<FriendItemUI>(friendsInformationPrefab, friendsContent);
            uiInstance.Setup(friend);
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
