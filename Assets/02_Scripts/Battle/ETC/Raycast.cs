using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Character"))
                    {
                        Debug.Log("Character Selected");
                        //TODO UI
                    }
                    else if (hit.transform.gameObject.CompareTag("Monster"))
                    {
                        Debug.Log("Monster Selected");
                        //TODO UI
                    }
                }
            }
        }
    }
}
