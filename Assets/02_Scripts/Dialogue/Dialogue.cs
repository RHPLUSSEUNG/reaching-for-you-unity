using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();

    Dictionary<string, DialogueNode> nodeLookUp = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR
    private void Awake()
    {
        if(nodes.Count == 0)
        {
            DialogueNode rootNode = new DialogueNode();
            rootNode.uniqueID = Guid.NewGuid().ToString();
            nodes.Add(rootNode);
        }
    }
#endif
    private void OnValidate()
    {
        nodeLookUp.Clear();

        foreach(DialogueNode node in nodes) 
        {
            nodeLookUp[node.uniqueID] = node;
        }
    }

    public void CreateRootNode()
    {
        if (nodeLookUp.Count == 0)
        {
            nodes.Add(new DialogueNode()
            {
                uniqueID = Guid.NewGuid().ToString()
            });
        }
    }

    public void CreateNode(DialogueNode parent)
    {
        DialogueNode newNode = new DialogueNode();
        newNode.uniqueID = Guid.NewGuid().ToString();
        parent.children.Add(newNode.uniqueID);
        nodes.Add(newNode);
        OnValidate();
    }

    public void DeleteNode(DialogueNode nodeToDelete)
    {
        nodes.Remove(nodeToDelete);
        OnValidate();
        CleanDanglingChildren(nodeToDelete);
    }

    public void CleanDanglingChildren(DialogueNode nodeToDialogue)
    {
        foreach(DialogueNode node in GetAllNodes())
        {
            node.children.Remove(nodeToDialogue.uniqueID);
        }
    }

    public IEnumerable<DialogueNode> GetAllNodes()
    {
        return nodes;
    }

    public DialogueNode GetRootNode()
    {
        return nodes[0];
    }

    public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
    {
        foreach (string childID in parentNode.children)
        {
            if (nodeLookUp.ContainsKey(childID))
            {
                yield return nodeLookUp[childID];
            }
        }

    }

    public class DialogueModificationProcessor : AssetPostprocessor //NMC
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string importedAsset in importedAssets)
            {
                Dialogue dialogue = AssetDatabase.LoadAssetAtPath(importedAsset, typeof(Dialogue)) as Dialogue;

                if (dialogue != null)
                    dialogue.CreateRootNode();
            }
        }
    }
}


