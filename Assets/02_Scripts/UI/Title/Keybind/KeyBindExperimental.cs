using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindExperimental : MonoBehaviour
{    
    void Update()
    {
        if (Input.GetKey(KeySetting.keys[KeyType.TOP]))
        {
            Debug.Log("UP");
        }
        if (Input.GetKey(KeySetting.keys[KeyType.BOTTOM]))
        {
            Debug.Log("BOTTOM");
        }
        if (Input.GetKey(KeySetting.keys[KeyType.LEFT]))
        {
            Debug.Log("LEFT");
        }
        if (Input.GetKey(KeySetting.keys[KeyType.RIGHT]))
        {
            Debug.Log("RIGHT");
        }

    }
}
