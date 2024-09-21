using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour
{
    [SerializeField] string friendName;
    [SerializeField] int friendID;
    [SerializeField] Image friendnPortrait;
    [SerializeField] int friendship = 1;    

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

    public Image GetFriendnPortrait()
    {
        return friendnPortrait;
    }

    public int GetFriendship()
    {
        return friendship;
    }
}
