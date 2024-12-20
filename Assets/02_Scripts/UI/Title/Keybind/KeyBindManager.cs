using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KeyType
{
    TOP,
    BOTTOM,
    LEFT,
    RIGHT,
    INTERACT,
    INFORMATION,
    MAX_NUM
}

public static class KeySetting
{
    public static Dictionary<KeyType, KeyCode> keys = new Dictionary<KeyType, KeyCode>();
}

public class KeyBindManager : MonoBehaviour
{    
    int key = -1;
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.E, KeyCode.Tab };

    void Awake()
    {
        for (int i = 0; i < (int)KeyType.MAX_NUM; i++)
        {
            if (!KeySetting.keys.ContainsKey((KeyType)i))
            {
                KeySetting.keys.Add((KeyType)i, defaultKeys[i]);
            }
        }
    }
    
    void OnGUI()
    {
        Event keyEvent = Event.current;
        
        if(keyEvent.isKey)
        {
            KeySetting.keys[(KeyType)key] = keyEvent.keyCode;
            key = -1;
        }
    }

    public void BindKey(int _key)
    {
        key = _key;
    }
}
