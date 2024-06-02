using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    public BoxCollider detector;
    public Collider[] colliders;
    int range;

    public void Awake()
    {
        detector = this.gameObject.GetComponent<BoxCollider>();
    }

    private void SetPosition(GameObject go)
    {
        this.gameObject.transform.position = go.transform.position;
    }

    private void SetRange(int range, TargetObject targetType)
    {
        Debug.Log($"Detect Target : {targetType}");
        detector.size = new Vector3(range, 3, range);
        this.range = range;
        colliders = Physics.OverlapBox(this.transform.position, new Vector3(range/2, 3/2, range/2), Managers.Battle.currentCharacter.transform.rotation, CalcLayerMask(targetType));

        foreach (Collider collider in colliders)
        {
            Debug.Log($"target list : {collider.gameObject}");
        }
    }

    private int CalcLayerMask(TargetObject targetType)
    {
        if(targetType == TargetObject.Me)
        {
            return 0;
        }
        int mask = 1 << 7;
        for(int i = 0; i < (int)targetType; i++)
        {
            mask <<= 1;
        }
        return mask;
    }

    public void SetDetector(GameObject go, int range, TargetObject targetType)
    {
        SetPosition(go);
        SetRange(range, targetType);
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
