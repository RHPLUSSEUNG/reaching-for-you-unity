using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConversant : MonoBehaviour
{
    [SerializeField] Dialogue dialogue = null;
    
    public bool StartDialogue(PlayerConversant playerConversant)
    {
        if(dialogue == null)
        {
            return false;
        }
        else
        {
            playerConversant.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);
            return true;
        }        
    }
}
