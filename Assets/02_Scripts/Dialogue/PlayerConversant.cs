using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{    
    Dialogue currentDialogue;
    DialogueNode currentNode = null;
    NPCConversant currentConversant = null;
    bool isChoosing = false;

    //private void Awake()
    //{
    //    currentNode = currentDialogue.GetRootNode();
    //}

    public event Action onConversationUpdated;
  

    public void StartDialogue(NPCConversant newConversant, Dialogue newDialogue)
    {
        currentConversant = newConversant;
        currentDialogue = newDialogue;
        currentNode = currentDialogue.GetRootNode();
        TriggerEnterAction();
        onConversationUpdated();
    }

    public void Quit()
    {        
        currentDialogue = null;
        TriggerExitAction();
        currentNode = null;
        isChoosing = false;
        currentConversant = null;
        onConversationUpdated();
    }

    public bool IsActive()
    {
        return currentDialogue != null;
    }

    public bool IsChoosing()
    {
        return isChoosing;
    }

    public string GetText()
    {
        if (currentDialogue == null)
        {
            return "current dialogue is null";
        }

        return currentNode.GetText();
    }

    public IEnumerable<DialogueNode> GetChoices()
    {
        return currentDialogue.GetPlayerChildren(currentNode);
    }

    public void SelectChoice(DialogueNode chosenNode)
    {
        currentNode = chosenNode;
        TriggerEnterAction();
        isChoosing = false;
        Next();
    }

    public void Next()
    {
        int numPlayerResponse = currentDialogue.GetPlayerChildren(currentNode).Count();
        
        if(numPlayerResponse > 0) 
        {
            isChoosing = true;
            TriggerExitAction();
            onConversationUpdated();
            return;
        }

        DialogueNode[] children = currentDialogue.GetNPCChildren(currentNode).ToArray();
        int randomIndex = UnityEngine.Random.Range(0, children.Count());
        TriggerExitAction();
        currentNode =  children[randomIndex];
        TriggerEnterAction();
        onConversationUpdated();
    }

    public bool HasNext()
    {        
        return currentDialogue.GetAllChildren(currentNode).Count() > 0;
    }

    void TriggerEnterAction()
    {
        if(currentNode != null)
        {
            TriggerAction(currentNode.GetOnEnterAction());
        }
    }

    void TriggerExitAction()
    {
        if (currentNode != null)
        {
            TriggerAction(currentNode.GetOnExitAction());
        }
    }

    void TriggerAction(string action)
    {
        if(action == "")
        {
            return;
        }

        foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
        {
            trigger.Trigger(action);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            other.GetComponent<NPCConversant>().StartDialogue(this);
        }
    }
}
