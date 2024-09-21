using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FriendshipManager : MonoBehaviour
{
    public static FriendshipManager Instance;
    List<Friend> friends = new List<Friend>();

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

    public void JoinToFriends(Friend _friend)
    {
        if(!friends.Contains(_friend))
        {
            friends.Add(_friend);
            onUpdate.Invoke();
        }        
    }

    public void FriendshipHandler(Friend _friend, int level)
    {
        if(friends.Contains(_friend))
        {            
            if (level > 0)
            {
                _friend.RiseFriendshipLevel(level);
            }
            else
            {
                _friend.FallFriendshipLevel(level);
            }
            onUpdate.Invoke();
        }
        else
        {
            Debug.Log("Friend is not in the list");
            return;
        }        
    }

    public List<Friend> GetFriends()
    {
        return friends;
    }
}
