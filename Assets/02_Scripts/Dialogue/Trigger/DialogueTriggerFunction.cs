using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCType
{
    NPC,
    BUDDY,
    ENEMY,
}

public class DialogueTriggerFunction : MonoBehaviour
{
    [SerializeField] NPCType type;
    [SerializeField]int friendShipLevel = 0;
    
    public void RiseFriendShipLevel(int level)
    {
        friendShipLevel += level;
        Debug.Log("Rise Friendship Level " + level);
    }

    public void FallFriendShipLevel(int level)
    {
        friendShipLevel -= level;
        Debug.Log("Fall Friendship Level " + level);
    }
}
