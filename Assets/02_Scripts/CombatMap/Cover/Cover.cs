using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : CoverData
{
    public GameObject[] coverPrefab;

    GameObject coverObject;

    private void Awake() {
        Init();
    }

    private void Init() 
    {
        SetStep(Random.Range(1, maxStep + 1));
        
        switch (GetStep())
        {
            case 1: // 1단계 (피격확률 80)
                damagePercent = 0.8f;
                hp = Random.Range(10, 41);
                coverObject = Instantiate(coverPrefab[0], transform);
                coverObject.transform.SetParent(transform);
                break;
            case 2: // 2단계 (피격확률 50)
                damagePercent = 0.5f;
                hp = Random.Range(40, 71);
                coverObject = Instantiate(coverPrefab[1], transform);
                coverObject.transform.SetParent(transform);
                break;
            case 3: // 3단계 (피격확률 0)
                damagePercent = 0f;
                hp = Random.Range(70, 101);
                coverObject = Instantiate(coverPrefab[2], transform);
                coverObject.transform.SetParent(transform);
                break;            
        }
    }

    public Cover returnCoverData()
    {
        return gameObject.GetComponent<Cover>();
    }

    public void AttackCover(int damage)
    {
        hp -= damage;

        CalculateHp();
        CalculateStep();
    }

    // 두 위치 간의 각도를 계산하는 메서드 (to: target)
    public float CalculateAngle(Vector3 fromPosition, Vector3 toPosition)
    {
        Vector3 direction = toPosition - fromPosition;
        float calculatedAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        calculatedAngle = Mathf.Abs(calculatedAngle);
        SetAngle(calculatedAngle);
        return calculatedAngle;
    }

    public void CheckTarget(int damage, Vector3 fromPosition, Vector3 toPosition)
    {
        // 각도 계산
        CalculateAngle(fromPosition, toPosition);

        // 각도에 따른 엄폐 확률
        float coverEffectiveness = CalculateCoverEffectiveness(GetAngle(), GetStep());

        // 플레이어가 엄폐 중? 엄폐 확률 계산 : 데미지 100%
        if (Random.value > coverEffectiveness) // 엄폐 효과에 따라 피격 확률 계산
        {
            Debug.Log("엄폐물로 인해 데미지가 들어가지 않음");
            // 해당 부분에서 엄폐물 hp 감소
            // 엄폐물 파괴 관련
            AttackCover(damage);
        }
        else
        {
            Debug.Log($"데미지가 {damagePercent * 100}%의 확률로 드감");
            // 해당 부분에서 플레이어 데미지 얻음
        }
    }

    public float CalculateCoverEffectiveness(float angle, int step)
    {
        // 각도와 단계에 따른 엄폐 효과 계산
        switch (step)
        {
            case 3:
                return 1f; // 3단계: 어떤 각도든지 데미지가 들어가지 않음

            case 2:
                if (angle <= 90f)
                {
                    return 0.1f; // 90도 이내일 때 엄폐 확률 10%
                }
                else if (angle <= 120f)
                {
                    return 0.4f; // 120도 이내일 때 엄폐 확률 40%
                }
                else if (angle <= 180f)
                {
                    return 0.5f; // 180도 이내일 때 엄폐 확률 50%
                }
                break;

            case 1:
                if (angle <= 90f)
                {
                    return 0f; // 90도 이내일 때 엄폐 확률 0%
                }
                else if (angle <= 120f)
                {
                    return 0.1f; // 120도 이내일 때 엄폐 확률 10%
                }
                else if (angle <= 180f)
                {
                    return 0.2f; // 180도 이내일 때 엄폐 확률 20%
                }
                break;
        }
        return 0f; // 그 외의 경우 엄폐 확률 0%
    }
}
