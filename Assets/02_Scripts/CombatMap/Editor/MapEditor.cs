using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (CreateObject)), CanEditMultipleObjects]
public class MapEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        // CreateObject map = target as CreateObject;

        // map.GenerateMap();
    }
}
