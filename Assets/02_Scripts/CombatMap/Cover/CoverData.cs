using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverData : MonoBehaviour
{
    public int hp;

    public int maxStep = 3;
    public int step;
    public int currentStep;

    public bool isHiding = false; // 플레이어가 엄폐하고 있는지 확인하는 변수, 기본값은 false

    public float damagePercent; // 피격 확률

    private float angle;

    public float GetAngle()
    {
        return angle;
    }

    public void SetAngle(float _angle)
    {
        angle = _angle;
    }
}
