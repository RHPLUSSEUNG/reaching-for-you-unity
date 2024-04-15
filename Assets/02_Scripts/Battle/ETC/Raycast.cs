using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    #region singleton
    static Raycast _instance;
    public static Raycast raycast { get { return _instance; } }

    #endregion
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Character"))
                {
                    Debug.Log("Character Selected : "+hit.collider.gameObject.name);

                    //TODO UI
                    PlayerButton.playerButton.UpdateButton(hit.collider.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("Monster"))
                {
                    Debug.Log("Monster Selected");
                    //TODO UI
                }
            }
        }
    }

    public Vector3 GetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return Input.mousePosition;
        }
        else return Vector3.zero;
    }


}
