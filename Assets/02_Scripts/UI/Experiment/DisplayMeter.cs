using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMeter : MonoBehaviour
{
    private float deltaTime = 0f;

    [SerializeField] private int size = 25;
    [SerializeField] private Color color = Color.red;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;

        // FPS and Frame Time
        float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string fpsText = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);

        // Fullscreen or Windowed
        string screenMode = Screen.fullScreen ? "Fullscreen" : "Windowed";

        // Current Resolution
        Resolution resolution = Screen.currentResolution;
        string resolutionText = string.Format("{0} x {1}", resolution.width, resolution.height);

        // Graphics Quality
        string qualityLevel = QualitySettings.names[QualitySettings.GetQualityLevel()];

        // Anti-Aliasing
        string antiAliasing = QualitySettings.antiAliasing == 0 ? "Off" : QualitySettings.antiAliasing.ToString() + "x";

        // Brightness (using exposure from post-processing if available, otherwise defaulting to 1.0f)
        float brightness = 1.0f;
        // If using Post-Processing with Exposure, you need to retrieve the value from there
        // Uncomment and modify the following lines if using a Post-Processing Stack
        // Exposure exposure;
        // if (postProcessVolume.profile.TryGetSettings(out exposure))
        // {
        //     brightness = exposure.keyValue.value;
        // }

        string brightnessText = string.Format("Brightness: {0:0.0}", brightness);

        // Combine all information
        string text = fpsText + "\n" +
                      "Screen Mode: " + screenMode + "\n" +
                      "Resolution: " + resolutionText + "\n" +
                      "Graphics Quality: " + qualityLevel + "\n" +
                      "Anti-Aliasing: " + antiAliasing + "\n" +
                      brightnessText;

        GUI.Label(rect, text, style);
    }
}
