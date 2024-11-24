using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MiniGame", menuName = "Scriptable Object/MiniGame")]
[System.Serializable]
public class MiniGameData : ScriptableObject
{
    [SerializeField]
    int gameID;
    [SerializeField]
    string gameName;
    [SerializeField]
    [TextArea]
    string gameDescript;
    [SerializeField]
    Sprite gameImg;

    public int GetGameID()
    {
        return gameID;
    }
    
    public string GetGameName()
    {
        return gameName;
    }
    
    public string GetGameDescript()
    {
        return gameDescript;
    }

    public Sprite GetGameImage()
    {
        return gameImg;
    }
}
