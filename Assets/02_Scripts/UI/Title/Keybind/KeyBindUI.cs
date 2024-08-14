using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBindUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] keyText;

    void Start()
    {
        for(int i = 0; i < keyText.Length; i++) 
        {
            keyText[i].text = KeySetting.keys[(KeyType)i].ToString();
        }
    }
    
    void Update()
    {
        for (int i = 0; i < keyText.Length; i++)
        {
            keyText[i].text = KeySetting.keys[(KeyType)i].ToString();
        }
    }
}
