using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoverData", menuName = "Reaching-for-you/CoverData")]
public class CoverData : ScriptableObject
{
    [SerializeField]
    private int hp;

    private int step;

    public GameObject coverGameObject;
    public Map map;
    public bool isHiding = false; // 플레이어가 엄폐하고 있는지 확인하는 변수, 기본값은 false

    private float damagePercent;

    GameObject character;
    CharacterState characterstate;
    EntityStat characterstat;

    public Vector3 playerPosition; // 플레이어의 위치를 저장하는 변수

    public void Init() {
        if(hp > 100) return;

        if(hp <= 70) 
        {
            step = 3;
            damagePercent = 1f;
        }
        
        else if(hp <= 40) 
        {
            step = 2;
            damagePercent = 0.8f;
        }

        else if(hp <= 10) 
        {
            step = 1;
            damagePercent = 0.5f;
        }

        else damagePercent = 0;
    }


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
    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        playerPosition = newPosition;

        // 엄폐물의 사방면을 확인하여 플레이어가 있는지 확인
        Vector3 coverPosition = coverGameObject.transform.position;
        if (IsPlayerNearCover(coverPosition, newPosition))
        {
            SetHiding(true);
        }
        else
        {
            SetHiding(false);
        }
    }

    // 플레이어가 엄폐물 근처에 있는지 확인하는 메서드
    private bool IsPlayerNearCover(Vector3 coverPosition, Vector3 playerPosition)
    {
        // 사방면을 확인
        if (playerPosition == coverPosition + Vector3.forward ||
            playerPosition == coverPosition + Vector3.back ||
            playerPosition == coverPosition + Vector3.left ||
            playerPosition == coverPosition + Vector3.right)
        {
            return true;
        }
        return false;
    }
}
