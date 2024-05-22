using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    RangeDetector detector;
    GameObject go;
    void Start()
    {
        GameObject character = GameObject.Find("Player_Girl_Battle");

        detector = GameObject.Find("RangeDetector").GetComponent<RangeDetector>();
        detector.SetDetector(character, 1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                go = hit.collider.gameObject;
                Debug.Log("ray :" + go);
                detector.Detect(go);
            }
        }
    }
}
