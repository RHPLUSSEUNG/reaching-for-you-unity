using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteSettingEditor : EditorWindow
{
    private float pixelsPerUnit = 100f; // �⺻ PPU ��
    private FilterMode filterMode = FilterMode.Bilinear; // �⺻ ���� ���
    private int maxSize = 1024; //�⺻ maxSize;

    [MenuItem("Tools/Batch Sprite Settings")]
    public static void ShowWindow()
    {
        GetWindow<SpriteSettingEditor>("Batch Sprite Settings");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Change Sprite Settings", EditorStyles.boldLabel);

        // �ȼ� �� ���� ����
        pixelsPerUnit = EditorGUILayout.FloatField("Pixels Per Unit", pixelsPerUnit);

        // ���� ��� ����
        filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", filterMode);

        maxSize = EditorGUILayout.IntField("Max Size", maxSize);

        if (GUILayout.Button("Apply Settings to All Sprites"))
        {
            ApplySettingsToAllSprites();
        }
    }

    private void ApplySettingsToAllSprites()
    {
        //��������Ʈ�� �˻�
        //���� ��������Ʈ
        string[] allSpritePaths = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/04_Images/Charcter/Enemy" });

        foreach (string spritePath in allSpritePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(spritePath);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter != null && textureImporter.textureType == TextureImporterType.Sprite)
            {
                // ��������Ʈ ���� ����
                textureImporter.spritePixelsPerUnit = pixelsPerUnit;
                textureImporter.filterMode = filterMode;
                textureImporter.maxTextureSize = maxSize;

                // ���� ���� ����
                textureImporter.SaveAndReimport();
                Debug.Log($"Updated sprite: {path}");
            }
        }

        Debug.Log("All sprite settings updated.");
    }
}
