using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverData : MonoBehaviour
{
    public int hp;

    public int maxStep = 3;
    public int step;

    RaycastHit hitInfo;

    public Map map;
    public bool isHiding = false; // 플레이어가 엄폐하고 있는지 확인하는 변수, 기본값은 false

    public float damagePercent;

    GameObject character;
    CharacterState characterstate;
    EntityStat characterstat;

    public Vector3 playerPosition; // 플레이어의 위치를 저장하는 변수

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

    public float GetDamagePercent()
    {
        return damagePercent;
    }

    public int GetStep()
    {
        return step;
    }

    public void AttackCover()
    {
        if (step > 0)
        {
            step--;
            CalculateStep();
        }
    }

    // 플레이어가 엄폐 상태인지 설정하는 메서드
    public void SetHiding(bool hiding)
    {
        isHiding = hiding;
    }

    // 두 위치 간의 각도를 계산하는 메서드
    public float CalculateAngle(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 direction = toPosition - fromPosition;
        float calculatedAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        calculatedAngle = Mathf.Abs(calculatedAngle);
        return calculatedAngle;
    }

    // 플레이어의 위치를 저장하고 엄폐 상태를 설정하는 메서드
    public void UpdatePlayerPosition(Vector3 coverGameObjectPos)
    {
        // 엄폐물의 사방면을 확인하여 플레이어가 있는지 확인
        if (IsPlayerNearCover(coverGameObjectPos))
        {
            SetHiding(true);
        }
        else
        {
            SetHiding(false);
        }
    }

    // 플레이어가 엄폐물 근처에 있는지 확인하는 메서드
    private bool IsPlayerNearCover(Vector3 coverPosition)
    {
        if(Physics.Raycast(coverPosition, Vector3.forward, out hitInfo, 1f) ||
            Physics.Raycast(coverPosition, Vector3.back, out hitInfo, 1f) ||
            Physics.Raycast(coverPosition, Vector3.left, out hitInfo, 1f) ||
            Physics.Raycast(coverPosition, Vector3.right, out hitInfo, 1f)) 
            {
                if(hitInfo.collider.tag == "Player")
                    return true;
            }
        return false;
    }
}
