using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    [SerializeField] string friendName;
    [SerializeField] int friendID;
    [SerializeField] Sprite friendPortrait;
    [SerializeField] int friendship = 0;    

    public void RiseFriendshipLevel(int level)
    {
        if(friendship + level >= 6)
        {
            friendship = 6;
        }
        else
        {
            friendship += level;
        }        
    }

    public void FallFriendshipLevel(int level)
    {
        if(friendship - level <= 0)
        {
            friendship = 0;
        }
        else
        {
            friendship -= level;
        }       
    }

    public string GetFriendName()
    {
        return friendName;
    }

    public int GetFriendID()
    {
        return friendID;
    }

    public Sprite GetFriendPortrait()
    {
        return friendPortrait;
    }

    public int GetFriendship()
    {
        return friendship;
    }
}
