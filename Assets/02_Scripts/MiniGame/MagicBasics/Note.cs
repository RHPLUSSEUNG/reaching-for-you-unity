using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;

    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }
}
