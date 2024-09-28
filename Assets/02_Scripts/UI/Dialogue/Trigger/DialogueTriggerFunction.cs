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

    public void JoinToFriend()
    {
        FriendshipManager.Instance.JoinToFriends(transform.GetComponent<Friend>());
    }

    public void ChangeFriendship(int level)
    {        
        FriendshipManager.Instance.FriendshipHandler(transform.GetComponent<Friend>(), level);
        Debug.Log("Rise Friendship Level " + level);
    }    

    public void EnterTheDungeon()
    {
        SceneChanger.Instance.ChangeScene(SceneType.PM_COMBAT);
    }    

    public void RequestItem()
    {
        GiftUI.Instance.ShowGiftPanel();
        GiftUI.Instance.ReportTargetNPC(transform.GetComponent<Friend>());
    }
}
