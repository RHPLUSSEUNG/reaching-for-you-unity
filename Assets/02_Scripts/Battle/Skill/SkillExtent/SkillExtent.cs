using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillExtent : MonoBehaviour
{
    [SerializeField]
    protected Collider[] colliders;
    [SerializeField]
    protected int range;
    [SerializeField]
    protected TargetObject targetType;

    [SerializeField]
    private List<GameObject> targets = new();
    float angle = 90f;
    Vector3 rotate;
    public List<GameObject> SetArea(int range, TargetObject targetType, Vector3 pos, bool Circular)
    {
        this.range = range;
        this.targetType = targetType;
        this.gameObject.transform.position = pos;
        targets.Clear();
        if(Circular)
        {
            rotate = Managers.Battle.currentCharacter.transform.position - pos;
            CircularCharacter();
        }
        else
        {
            RectCharacter();
        }
        return targets;
    }


    protected void CircularCharacter()
    {
        GetCharacter();
        transform.position = Managers.Battle.currentCharacter.transform.position;
        foreach (Collider c in colliders)
        {
            Vector3 interV = c.transform.position - transform.position;

            if(rotate.magnitude <= range)
            {
                //Debug.Log("rotate magnitude");
                float dot = Vector3.Dot(interV.normalized, rotate);
                float theta = Mathf.Acos(dot);
                float degree = Mathf.Rad2Deg * theta;
                //Debug.Log($"Degree : {degree}");
                //Debug.Log($"Standard : {range / 2f}");
                if (degree <= range / 2f)
                    targets.Add(c.gameObject.transform.parent.gameObject);
            }
        }
    }

    protected void RectCharacter()
    {
        GetCharacter();
        if(targetType == TargetObject.Tile)
        {
            foreach (Collider c in colliders)
            {
                targets.Add(c.gameObject);
            }
        }
        else
        {
            foreach (Collider c in colliders)
            {
                targets.Add(c.gameObject.transform.parent.gameObject);
            }
        }
        
    }

    protected void GetCharacter()
    {
        colliders = Physics.OverlapBox(this.transform.position, new Vector3(range, 3, range), new Quaternion(0,0,0,0), CalcLayer(targetType));
    }

    protected int CalcLayer(TargetObject targetType)
    {
        if (targetType == TargetObject.Me)
        {
            return 0;
        }
        int mask = 1 << 7;
        for (int i = 0; i < (int)targetType; i++)
        {
            mask <<= 1;
        }
        return mask;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, rotate, angle / 2, (float)range);
        Handles.DrawSolidArc(transform.position, Vector3.up, rotate, -angle / 2, (float)range);
    }
#endif
}
