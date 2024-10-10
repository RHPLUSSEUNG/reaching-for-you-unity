using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] string playerName;
    AMCharacterData playerCharacterData;

    Dialogue currentDialogue;
    DialogueNode currentNode = null;
    NPCConversant currentConversant = null;

    bool isChoosing = false;    
    PlayerController playerController;

    public event Action onConversationUpdated;

    private void Awake()
    {        
        playerController = GetComponentInParent<PlayerController>();
        playerCharacterData = GetComponent<CharcterDataHandler>().GetCharacterData();
    }    

    public void StartDialogue(NPCConversant newConversant, Dialogue newDialogue)
    {
        playerController.ChangeActive(false);
        currentConversant = newConversant;
        currentDialogue = newDialogue;
        currentNode = currentDialogue.GetRootNode();
        TriggerEnterAction();
        onConversationUpdated();
    }

    public void Quit()
    {
        playerController.ChangeActive(true);
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

    public string GetCurrentConversantName()
    {
        if(isChoosing)
        {
            return playerName;
        }
        else
        {
            return currentConversant.GetName();
        }
    }

    public IEnumerable<DialogueNode> GetChoices()
    {
        return FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode));
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
        if(!GiftUI.Instance.GetGiftActive())
        {
            int numPlayerResponse = FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).Count();

            if (numPlayerResponse > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }

            DialogueNode[] children = FilterOnCondition(currentDialogue.GetNPCChildren(currentNode)).ToArray();
            if (children.Count() == 0)
            {
                Quit();
                return;
            }
            int randomIndex = UnityEngine.Random.Range(0, children.Count());
            TriggerExitAction();
            currentNode = children[randomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }        
    }

    public bool HasNext()
    {        
        return FilterOnCondition(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
    }

    IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
    {
        foreach(var node in inputNode)
        {
            if(node.CheckCondition(GetEvaluators()))
            {
                yield return node;
            }
        }
    }

    IEnumerable<IPredicateEvaluator> GetEvaluators()
    {
        return GetComponents<IPredicateEvaluator>();
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

    void TriggerAction(TriggerActionList action)
    {
        if(action == TriggerActionList.NONE)
        {
            return;
        }

        foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
        {
            trigger.Trigger(action);
        }
    }

    public Sprite GetPlayerExpression(int index)
    {
        return playerCharacterData.GetExpression(index);
    }

    public DialogueNode GetCurrentNode()
    {
        return currentNode;
    }

    public DialogueNode GetNextNode()
    {
        DialogueNode[] children = FilterOnCondition(currentDialogue.GetPlayerChildren(currentNode)).ToArray();
        if (children.Length > 0)
        {
            return children[0];
        }
        return null;
    }

    public Sprite GetNPCExpression(int index)
    {
        if (currentConversant != null)
        {
            return currentConversant.GetNPCExpression(index);
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            other.GetComponent<NPCConversant>().SetActionButton(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            other.GetComponent<NPCConversant>().OffButton();
        }
    }
}
