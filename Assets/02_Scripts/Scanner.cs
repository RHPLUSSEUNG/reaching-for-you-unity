using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float transparency =1.0f;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggerd");
        ChangeAlpha(other, transparency);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("triggerd");
        ChangeAlpha(other, 1);
    }
    // Update is called once per frame
    void ChangeAlpha(Collider obj,float transparency)
    {
        Material material = obj.GetComponent<Renderer>().material;
        material.color= new Color(material.color.r, material.color.g, material.color.b, transparency);
    }
}
