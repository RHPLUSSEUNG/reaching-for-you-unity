using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.MPE;
using UnityEngine;


public class DialogueEditor : EditorWindow
{
    Dialogue selectedDialogue = null;
    [NonSerialized] GUIStyle nodeStyle;
    [NonSerialized] GUIStyle playerNodeStyle;
    [NonSerialized] DialogueNode draggingNode = null;
    [NonSerialized] Vector2 draggingOffset;
    [NonSerialized] DialogueNode creatingNode = null;
    [NonSerialized] DialogueNode deletingNode = null;
    [NonSerialized] DialogueNode linkingParentNode = null;
    Vector2 scrollPosition;
    [NonSerialized] bool draggingCanvas = false;
    [NonSerialized] Vector2 draggingCanvasOffset;

    const float canvasSize = 5000;
    const float backgroundSize = 50;
    [MenuItem("Window/Dialogue Editor")] //Callback
    public static void ShowEditorWindow()
    {
        GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
    }

    [OnOpenAssetAttribute(1)]
    public static bool OpenDialogue(int instanceID, int line)
    {
        Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue; //Ä³½ºÆÃ

        if (dialogue != null)
        {
            ShowEditorWindow();
            return true;
        }

        return false;
    }

    private void OnEnable()
    {
        Selection.selectionChanged += OnSelectionChanged;

        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
        nodeStyle.normal.textColor = Color.white;
        nodeStyle.padding = new RectOffset(20, 20, 20, 20);
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        playerNodeStyle = new GUIStyle();
        playerNodeStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
        playerNodeStyle.normal.textColor = Color.white;
        playerNodeStyle.padding = new RectOffset(20, 20, 20, 20);
        playerNodeStyle.border = new RectOffset(12, 12, 12, 12);
    }

    private void OnSelectionChanged()
    {
        Dialogue newDialogue = Selection.activeObject as Dialogue;

        if (newDialogue != null)
        {
            selectedDialogue = newDialogue;
            Repaint();
        }
    }

    private void OnGUI()
    {
        if (selectedDialogue == null)
        {
            EditorGUILayout.LabelField("No Dialogue Selected.");
        }
        else
        {
            ProcessEvents();

            if (selectedDialogue.GetAllNodes().Count() == 0)
            {
                selectedDialogue.CreateNode(null);
            }
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);            

            Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);
            Texture2D backgroundTexture = Resources.Load("background") as Texture2D;
            Rect textureCoords = new Rect(0, 0, canvasSize / backgroundSize, canvasSize / backgroundSize);
            GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, textureCoords);

            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawConnections(node);
            }
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                DrawNode(node);
            }

            EditorGUILayout.EndScrollView();

            if (creatingNode != null)
            {
                selectedDialogue.CreateNode(creatingNode);
                creatingNode = null;
            }
            if (deletingNode != null)
            {               
                selectedDialogue.DeleteNode(deletingNode);
                deletingNode = null;
            }
        }
    }

    void ProcessEvents()
    {
        if (Event.current.type == EventType.MouseDown && draggingNode == null)
        {
            draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);

            if (draggingNode != null)
            {
                draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                Selection.activeObject = draggingNode;
            }
            else
            {
                draggingCanvas = true;
                draggingCanvasOffset = Event.current.mousePosition + scrollPosition;
                Selection.activeObject = selectedDialogue;
            }            
        }
        else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
        {            
            draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
            
            GUI.changed = true;
        }
        else if (Event.current.type == EventType.MouseDrag && draggingCanvas)
        {
            scrollPosition = draggingCanvasOffset - Event.current.mousePosition;

            GUI.changed = true;
        }
        else if (Event.current.type == EventType.MouseUp && draggingNode != null)
        {
            draggingNode = null;
        }
        else if (Event.current.type == EventType.MouseUp && draggingCanvas)
        {
            draggingCanvas = false;
        }
    }

    void DrawNode(DialogueNode node)
    {
        GUIStyle style = nodeStyle;

        if(node.IsPlayerSpeaking())
        {
            style = playerNodeStyle;
        }

        GUILayout.BeginArea(node.GetRect(), style);
        EditorGUI.BeginChangeCheck();

        node.SetText(EditorGUILayout.TextField(node.GetText()));

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("x"))
        {
            deletingNode = node;
        }

        DrawLinkButtons(node);

        if (GUILayout.Button("+"))
        {
            creatingNode = node;
        }

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }

    void DrawLinkButtons(DialogueNode node)
    {
        if (linkingParentNode == null)
        {
            if (GUILayout.Button("link"))
            {
                linkingParentNode = node;
            }
        }
        else if(linkingParentNode == node)
        {
            if (GUILayout.Button("cancel"))
            {
                linkingParentNode = null;
            }
        }
        else if(linkingParentNode.GetChildren().Contains(node.name))
        {
            if (GUILayout.Button("unlink"))
            {                
                linkingParentNode.RemoveChild(node.name);
                linkingParentNode = null;
            }
        }
        else
        {
            if (GUILayout.Button("child"))
            {                
                linkingParentNode.AddChild(node.name);
                linkingParentNode = null;
            }
        }
    }

    void DrawConnections(DialogueNode node)
    {
        Vector3 startPosition = new Vector2(node.GetRect().xMax, node.GetRect().center.y);

        foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
        {
            Vector3 endPosition = new Vector2(childNode.GetRect().xMin, childNode.GetRect().center.y);
            Vector3 controlPointOffset = endPosition - startPosition;
            controlPointOffset.y = 0;
            controlPointOffset.x *= 0.8f;
            Handles.DrawBezier(startPosition, endPosition, startPosition + controlPointOffset, endPosition - controlPointOffset, Color.yellow, null, 4f);
        }
    }

    DialogueNode GetNodeAtPoint(Vector2 point)
    {
        DialogueNode foundNode = null;

        foreach (DialogueNode node in selectedDialogue.GetAllNodes())
        {
            if (node.GetRect().Contains(point))
            {
                foundNode = node;
            }
        }

        return foundNode;
    }
}