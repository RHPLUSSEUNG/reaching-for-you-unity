using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendData : MonoBehaviour
{
    [SerializeField] int friendship = 0;
    [SerializeField] GameObject friendshipEffect;
    [SerializeField] string characterName;
    [SerializeField] int characterID;
    [SerializeField] Sprite characterPortrait;    
    [SerializeField] List<Sprite> expressions = new List<Sprite>();

    public string GetCharacterName()
    {
        return characterName;
    }

    public int GetCharacterID()
    {
        return characterID;
    }

    public Sprite GetCharacterPortrait()
    {
        return characterPortrait;
    }

    public Sprite GetExpression(int index)
    {
        if (index >= 0 && index < expressions.Count)
        {
            return expressions[index];
        }
        return null;
    }

    void Start()
    {
        friendshipEffect.SetActive(false);
    }   

    public void RiseFriendshipLevel(int level)
    {
        if(friendship + level >= 6)
        {
            friendship = 6;
        }
        else
        {
            friendship += level;
            ActiveFriendshipEffect();
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

    public int GetFriendship()
    {
        return friendship;
    }

    void ActiveFriendshipEffect()
    {
        friendshipEffect.SetActive(true);
    }
}
