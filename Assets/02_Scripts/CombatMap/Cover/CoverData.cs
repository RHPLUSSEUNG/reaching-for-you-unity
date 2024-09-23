using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverData : MonoBehaviour
{
    private int hp;

    public int maxStep = 3;
    private int step;

    public bool isHiding = false; // 플레이어가 엄폐하고 있는지 확인하는 변수, 기본값은 false

    public float damagePercent; // 피격 확률

    private float angle;

    public void CalculateStep() 
    {
        switch (step)
        {
            case 0:
                damagePercent = 1f;
                break;
            case 1:
                damagePercent = 0.8f;
                break;
            case 2:
                damagePercent = 0.5f;
                break;
            case 3:
                damagePercent = 0f;
                break;
        }
    }

    public void CalculateHp()
    {
        if(hp >= 70)
            step = 3;
        else if(hp >= 40)
            step = 2;
        else if(hp >= 10)
            step = 1;
        else 
            step = 0;
    }

    public void AttackCover(int damage)
    {
        hp -= damage;

        CalculateHp();
        CalculateStep();
    }

    public int GetStep()
    {
        return step;
    }

    public void SetStep(int _step)
    {
        step = _step;
    }

    public int GetHp()
    {
        return hp;
    }

    public void SetHp(int _hp)
    {
        hp = _hp;
    }

    public float GetAngle()
    {
        return angle;
    }

    public void SetAngle(float _angle)
    {
        angle = _angle;
    }
}
