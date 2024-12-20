using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public enum Expression
{
    DEFAULT,
    ANGRY,
    SAD,
    SURPRISED,
    HAPPY
}
public class DialogueNode : ScriptableObject
{
    [SerializeField] bool isPlayerSpeaking = false;    
    [SerializeField] string text;
    [SerializeField] List<string> children = new List<string>();
    [SerializeField] Rect rect = new Rect(0, 0, 200, 100);
    [SerializeField] TriggerActionList onEnterAction;
    [SerializeField] TriggerActionList onExitAction;
    [SerializeField] Condition condition;
    [SerializeField] Expression characterExpression = Expression.DEFAULT;

    public Rect GetRect()
    {
        return rect;
    }

    public string GetText()
    {
        return text;
    }

    public List<string> GetChildren()
    {
        return children;
    }

    public bool IsPlayerSpeaking()
    {
        return isPlayerSpeaking;
    }

    public TriggerActionList GetOnEnterAction()
    {
        return onEnterAction;
    }

    public TriggerActionList GetOnExitAction()
    {
        return onExitAction;
    }

    public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
    {
        return condition.Check(evaluators);
    }

    public Expression GetCharacterExpression()
    {
        return characterExpression;
    }

#if UNITY_EDITOR
    public void SetPosition(Vector2 newPosition)
    {
        Undo.RecordObject(this, "Move Dialogue Node");
        rect.position = newPosition;
        EditorUtility.SetDirty(this);
    }

    public void SetText(string newText)
    {
        if(newText != text) 
        {
            Undo.RecordObject(this, "Update Dialogue Text");
            text = newText;
            EditorUtility.SetDirty(this);
        }        
    }

    public void AddChild(string childID)
    {
        Undo.RecordObject(this, "Add Dialogue Link");
        children.Add(childID);
        EditorUtility.SetDirty(this);
    }

    public void RemoveChild(string childID) 
    {
        Undo.RecordObject(this, "Remove Dialogue Link");
        children.Remove(childID);
        EditorUtility.SetDirty(this);
    }

    public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
    {
        Undo.RecordObject(this, "Change Dialogue Speaker");
        isPlayerSpeaking = newIsPlayerSpeaking;
        EditorUtility.SetDirty(this);
    }

    public void SetCharacterExpression(Expression newExpression)
    {
        Undo.RecordObject(this, "Update Character Expresion");
        characterExpression = newExpression;
        EditorUtility.SetDirty(this);
    }
#endif

}
