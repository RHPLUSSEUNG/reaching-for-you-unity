using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //[Prototype2SH]

public enum NPCType
{
    NPC,
    FRIENDS,
    ENEMY,
}

public class DialogueTriggerFunction : MonoBehaviour
{
    [SerializeField] NPCType type;
    [SerializeField]int friendShipLevel = 0;
    AMCharacterData characterData;
    public void JoinToFriend()
    {
        characterData = GetComponent<CharcterDataHandler>().GetCharacterData();
        FriendshipManager.Instance.JoinToFriends(characterData);
    }

    public void ChangeFriendship(int level)
    {        
        FriendshipManager.Instance.FriendshipHandler(characterData, level);
        Debug.Log("Rise Friendship Level " + level);
    }    

    public void EnterTheDungeon()
    {
        SceneChanger.Instance.ChangeScene(SceneType.PM_COMBAT);
    }    

    public void SelectAdventureMap()
    {
        AdventureMapSelectUI.Instance.OpenPanel();
    }

    public void RequestItem()
    {
        GiftUI.Instance.ShowGiftPanel();
        GiftUI.Instance.ReportTargetNPC(characterData);
    }
}
