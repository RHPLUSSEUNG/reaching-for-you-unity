using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillExtent : MonoBehaviour
{
    protected Collider[] colliders;
    protected int range;
    protected TargetObject targetType;


    private List<GameObject> targets = new();
    float angle = 90f;
    Vector3 rotate;

    public List<GameObject> SetArea(int range, TargetObject targetType, Vector3 pos, bool Circular)
    {
        this.range = range;
        this.targetType = targetType;
        this.gameObject.transform.position = pos;

        if(Circular)
        {
            rotate = Managers.Battle.currentCharacter.transform.position - pos;
            CircularCharacter();
        }
        else
        {
            GetCharacter();
        }
        return targets;
    }


    protected void CircularCharacter()
    {
        GetCharacter();
        transform.position = Managers.Battle.currentCharacter.transform.position;
        foreach (Collider c in colliders)
        {
            GameObject go = c.gameObject;

            Vector3 interV = go.transform.position - transform.position;

            if(rotate.magnitude <= range)
            {
                float dot = Vector3.Dot(interV.normalized, rotate);
                float theta = Mathf.Acos(dot);
                float degree = Mathf.Rad2Deg * theta;

                if(degree <= range / 2f)
                    targets.Add(go);
            }
        }
    }

    protected void RectCharacter()
    {
        GetCharacter();
        foreach (Collider c in colliders)
        {
            targets.Add(c.gameObject);
        }
    }

    protected void GetCharacter()
    {
        colliders = Physics.OverlapBox(this.transform.position, new Vector3(range / 2, 3 / 2, range / 2), Managers.Battle.currentCharacter.transform.rotation, CalcLayer(targetType));
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
