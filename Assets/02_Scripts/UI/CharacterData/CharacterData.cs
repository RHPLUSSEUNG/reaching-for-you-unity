using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] int characterID;
    [SerializeField] Sprite characterPortrait;    
    [SerializeField] GameObject friendshipEffect;
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

}
