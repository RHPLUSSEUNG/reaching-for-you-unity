using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBattle : MonoBehaviour
{
    public CreateObject createObject;

    void Start() {
        // UpdatePlayerPosition(transform.position); // 시작 시 플레이어 위치 설정

        Debug.Log(gameObject.transform.position);
        
    }

    private void Update() {
        
    }

    public void UpdatePlayerPosition(Vector3 newPosition) // 현재 위치 갱신
    {
        // for (int x = 0; x < createObject.Width; x++)
        // {
        //     for (int z = 0; z < createObject.Height; z++)
        //     {
        //         if (map[x, z].eObstacle == EObstacle.Obstacle)
        //         {
        //             CoverData coverData = GetCoverDataAtPosition(map[x, z].ObjectLocation);
        //             if (coverData != null)
        //             {
        //                 coverData.UpdatePlayerPosition(newPosition);
        //             }
        //         }
        //     }
        // }
    }

    void CheckTarget(Vector3 targetPosition) {
        Map[,] map = createObject.GetMap();
        int x = Mathf.RoundToInt(targetPosition.x);
        int z = Mathf.RoundToInt(targetPosition.z);

        if (x >= 0 && x < map.GetLength(0) && z >= 0 && z < map.GetLength(1))
        {
            Map targetMap = map[x, z];
            if (targetMap.eObstacle == EObstacle.Obstacle)
            {
                CoverData coverData = GetCoverDataAtPosition(targetMap.ObjectLocation);
                if (coverData != null)
                {
                    Vector3 playerPosition = coverData.playerPosition; // 플레이어 위치 가져오기
                    float damagePercent = coverData.GetDamagePercent();

                    // 플레이어와 적 사이 각도 계산
                    float coverAngle = coverData.CalculateAngle(coverData.coverGameObject.transform.position, playerPosition);

                    // 각도에 따른 엄폐 확률
                    float coverEffectiveness = CalculateCoverEffectiveness(coverAngle, coverData.GetStep());

                    // 플레이어가 엄폐 중? 엄폐 확률 계산 : 데미지 100%
                    if (coverData.isHiding && Random.value > coverEffectiveness) // 엄폐 효과에 따라 피격 확률 계산
                    {
                        Debug.Log("엄폐물로 인해 데미지가 들어가지 않음");
                    }
                    else
                    {
                        Debug.Log($"데미지가 {damagePercent * 100}%의 확률로 드감");
                    }
                }
            }
        }
    }

    void AttackCover(Vector3 targetPosition)
    {
        Map[,] map = createObject.GetMap();
        int x = Mathf.RoundToInt(targetPosition.x);
        int z = Mathf.RoundToInt(targetPosition.z);

        if (x >= 0 && x < map.GetLength(0) && z >= 0 && z < map.GetLength(1))
        {
            Map targetMap = map[x, z];
            if (targetMap.eObstacle == EObstacle.Obstacle)
            {
                CoverData coverData = GetCoverDataAtPosition(targetMap.ObjectLocation);
                if (coverData != null)
                {
                    coverData.AttackCover();
                    Debug.Log("엄폐물을 공격하여 step이 감소함");
                }
            }
        }
    }

    CoverData GetCoverDataAtPosition(Vector3 position)
    {
        Map[,] map = createObject.GetMap();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int z = 0; z < map.GetLength(1); z++)
            {
                if (map[x, z].ObjectLocation == position)
                {
                    // foreach (CoverData coverData in createObject.coverDataArray)
                    // {
                    //     if (coverData.map.ObjectLocation == position)
                    //     {
                    //         return coverData;
                    //     }
                    // }
                }
            }
        }
        return null;
    }

    float CalculateCoverEffectiveness(float angle, int step)
    {
        // 각도와 단계에 따른 엄폐 효과 계산
        switch (step)
        {
            case 3:
                return 1f; // 3단계: 어떤 각도든지 데미지가 들어가지 않음

            case 2:
                if (angle <= 90f)
                {
                    return 0.1f; // 90도 이내일 때 엄폐 확률 90%
                }
                else if (angle <= 120f)
                {
                    return 0.4f; // 120도 이내일 때 엄폐 확률 60%
                }
                else if (angle <= 180f)
                {
                    return 0.5f; // 180도 이내일 때 엄폐 확률 50%
                }
                break;

            case 1:
                if (angle <= 90f)
                {
                    return 0f; // 90도 이내일 때 엄폐 확률 100%
                }
                else if (angle <= 120f)
                {
                    return 0.1f; // 120도 이내일 때 엄폐 확률 90%
                }
                else if (angle <= 180f)
                {
                    return 0.2f; // 180도 이내일 때 엄폐 확률 80%
                }
                break;
        }
        return 0f; // 그 외의 경우 엄폐 확률 10%
    }
}
