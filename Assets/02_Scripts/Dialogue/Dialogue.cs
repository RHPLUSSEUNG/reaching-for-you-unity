using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();

    Dictionary<string, DialogueNode> nodeLookUp = new Dictionary<string, DialogueNode>();

    private void OnValidate()
    {
        if (nodes[0] == null)
        {
            return;
        }

        nodeLookUp.Clear();

        foreach (DialogueNode node in GetAllNodes())
        {
            nodeLookUp[node.name] = node;
        }
    }

    public void CreateRootNode()
    {
        if (nodeLookUp.Count == 0)
        {
            nodes.Add(new DialogueNode()
            {
                name = Guid.NewGuid().ToString()
            });
        }
    }

#if UNITY_EDITOR
    public void CreateNode(DialogueNode parent)
    {
        DialogueNode newNode = MakeNode(parent);
        Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
        Undo.RecordObject(this, "Added Dialogue Node");
        AddNode(newNode);
    }
       
    public void DeleteNode(DialogueNode nodeToDelete)
    {
        Undo.RecordObject(this, "Deleted Dialogue Node");
        nodes.Remove(nodeToDelete);        
        OnValidate();
        CleanDanglingChildren(nodeToDelete);
        Undo.DestroyObjectImmediate(nodeToDelete);
    }

    private static DialogueNode MakeNode(DialogueNode parent)
    {
        Vector2 rectOffset = new Vector2(200, 0);
        DialogueNode newNode = CreateInstance<DialogueNode>();
        newNode.name = Guid.NewGuid().ToString();


        if (parent != null)
        {
            parent.AddChild(newNode.name);
            newNode.SetPosition(parent.GetRect().position + rectOffset);
            newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
        }

        return newNode;
    }

    private void AddNode(DialogueNode newNode)
    {
        nodes.Add(newNode);
        OnValidate();
    }

    public void CleanDanglingChildren(DialogueNode nodeToDialogue)
    {
        foreach(DialogueNode node in GetAllNodes())
        {
            node.RemoveChild(nodeToDialogue.name);
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
        foreach (string childID in parentNode.GetChildren())
        {
            if (nodeLookUp.ContainsKey(childID))
            {
                yield return nodeLookUp[childID];
            }
        }
    }

    public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
    {
        foreach(DialogueNode node in GetAllChildren(currentNode))
        {
            if(node.IsPlayerSpeaking())
            {
                yield return node;
            }
        }
    }

    public IEnumerable<DialogueNode> GetNPCChildren(DialogueNode currentNode)
    {
        foreach (DialogueNode node in GetAllChildren(currentNode))
        {
            if (!node.IsPlayerSpeaking())
            {
                yield return node;
            }
        }
    }

#endif

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR
        if (nodes.Count == 0)
        {
            DialogueNode newNode = MakeNode(null);           
            AddNode(newNode);
        }

        if (AssetDatabase.GetAssetPath(this) != "")
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                if (AssetDatabase.GetAssetPath(node) == "")
                    AssetDatabase.AddObjectToAsset(node, this);
            }

            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

        }
#endif
    }

    public void OnAfterDeserialize()
    {
        
    }

    
}