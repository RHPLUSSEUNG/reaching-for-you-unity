using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerConversant : MonoBehaviour
{
    [SerializeField] Dialogue currentDialogue;
    DialogueNode currentNode = null;
    bool isChoosing = false;

    private void Awake()
    {
        currentNode = currentDialogue.GetRootNode();
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

    public void Next()
    {
        int numPlayerResponse = currentDialogue.GetPlayerChildren(currentNode).Count();
        
        if(numPlayerResponse > 0) 
        {
            isChoosing = true;
            return;
        }

        DialogueNode[] children = currentDialogue.GetNPCChildren(currentNode).ToArray();
        int randomIndex = Random.Range(0, children.Count());
        currentNode =  children[randomIndex];
    }

    public bool HasNext()
    {        
        return currentDialogue.GetAllChildren(currentNode).Count() > 0;
    }
}
