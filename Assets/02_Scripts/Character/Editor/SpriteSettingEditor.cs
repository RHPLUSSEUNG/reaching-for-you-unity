using System.IO;
using UnityEditor;
using UnityEngine;

public class SpriteSettingEditor : EditorWindow
{
    private float pixelsPerUnit = 100f; // 기본 PPU 값
    private FilterMode filterMode = FilterMode.Bilinear; // 기본 필터 모드
    private int maxSize = 1024; //기본 maxSize;

    [MenuItem("Tools/Batch Sprite Settings")]
    public static void ShowWindow()
    {
        GetWindow<SpriteSettingEditor>("Batch Sprite Settings");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Change Sprite Settings", EditorStyles.boldLabel);

        // 픽셀 퍼 유닛 설정
        pixelsPerUnit = EditorGUILayout.FloatField("Pixels Per Unit", pixelsPerUnit);

        // 필터 모드 설정
        filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", filterMode);

        maxSize = EditorGUILayout.IntField("Max Size", maxSize);

        if (GUILayout.Button("Apply Settings to All Sprites"))
        {
            ApplySettingsToAllSprites();
        }
    }

    private void ApplySettingsToAllSprites()
    {
        //스프라이트를 검색
        //몬스터 스프라이트
        string[] allSpritePaths = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/04_Images/Charcter/Enemy" });

        foreach (string spritePath in allSpritePaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(spritePath);
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            if (textureImporter != null && textureImporter.textureType == TextureImporterType.Sprite)
            {
                // 스프라이트 설정 변경
                textureImporter.spritePixelsPerUnit = pixelsPerUnit;
                textureImporter.filterMode = filterMode;
                textureImporter.maxTextureSize = maxSize;

                // 변경 사항 적용
                textureImporter.SaveAndReimport();
                Debug.Log($"Updated sprite: {path}");
            }
        }

        Debug.Log("All sprite settings updated.");
    }
}
