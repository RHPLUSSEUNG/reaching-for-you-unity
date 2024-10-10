using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FriendshipManager : MonoBehaviour
{
    public static FriendshipManager Instance;
    List<AMCharacterData> friends = new List<AMCharacterData>();

    public event Action onUpdate;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void JoinToFriends(AMCharacterData _friend)
    {
        if(!friends.Contains(_friend))
        {
            friends.Add(_friend);
            onUpdate?.Invoke();
        }        
    }

    public void FriendshipHandler(AMCharacterData _friend, int level)
    {
        AMFriendData friend = _friend as AMFriendData;
        if (friends.Contains(_friend))
        {            
            if (level > 0)
            {
                friend.RiseFriendshipLevel(level);
            }
            else
            {
                friend.FallFriendshipLevel(level);
            }
            onUpdate?.Invoke();
        }
        else
        {
            Debug.Log("Friend is not in the list");
            return;
        }        
    }

    public List<AMCharacterData> GetFriends()
    {
        return friends;
    }
}
