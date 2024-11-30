using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Friend", menuName = "Character/Friend")]
[System.Serializable]
public class AMFriendData : AMCharacterData
{
    [SerializeField] int friendship = 0;
    [SerializeField] List<int> preferenceItemID = new List<int>();

    public void RiseFriendshipLevel(int level)
    {
        if (friendship + level >= 6)
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
        if (friendship - level <= 0)
        {
            friendship = 0;
        }
        else
        {
            friendship -= level;
        }
    }

    public int GetFriendship()
    {
        return friendship;
    }
    
    public List<int> GetPreferenceItemID()
    {
        return preferenceItemID;
    }
}
