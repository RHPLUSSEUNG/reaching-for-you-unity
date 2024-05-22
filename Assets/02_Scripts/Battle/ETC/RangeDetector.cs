using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    BoxCollider detector;
    public Collider[] colliders;
    int range;

    public void Start()
    {
        detector = this.gameObject.GetComponent<BoxCollider>();
    }

    private void SetPosition(GameObject go)
    {
        this.gameObject.transform.position = go.transform.position;
    }

    private void SetRange(int range)
    {
        detector.size = new Vector3(range, 3, range);
        this.range = range;
        colliders = Physics.OverlapBox(this.transform.position, detector.size);

        foreach (Collider collider in colliders)
        {
            Debug.Log($"{collider.gameObject}");
        }
    }

    public void SetDetector(GameObject go, int range)
    {
        SetPosition(go);
        SetRange(range);
    }

    public bool Detect(GameObject go)
    {
        if (go == null && go == this.gameObject)
        {
            Debug.Log(go);
            return false;
        }
        foreach(Collider collider in colliders)
        {
            if(collider.gameObject == go)
            {
                Debug.Log("Detect In Range");
                return true;
            }
        }
        Debug.Log("Fail to Detect");
        return false;
    }
}
