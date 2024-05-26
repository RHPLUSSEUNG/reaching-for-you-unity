using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverBattle : MonoBehaviour
{
    public CreateObject createObject;

    private Vector3 currentPosition;

    void Start() {
        currentPosition = transform.position; // 현재 위치 설정
    }

    public void UpdateCurrentPosition(Vector3 newPosition) // 현재 위치 갱신
    {
        currentPosition = newPosition;
    }

    void CheckTarget(Vector3 targetPosition) {
        Map[,] map = createObject.GetMap();
        Vector3 difference = targetPosition - currentPosition;
        int x = Mathf.RoundToInt(targetPosition.x);
        int z = Mathf.RoundToInt(targetPosition.z);

        if (x >= 0 && x < map.GetLength(0) && z >= 0 && z < map.GetLength(1))
        {
            Map targetMap = map[x, z];
            if (targetMap.eObstacle == EObstacle.Obstacle)
            {
                // 위치 더 추가해야 됨
                if (difference == new Vector3(0, -1, 0))
                {
                    AttackCover(targetPosition);
                    Debug.Log("엄폐물로 인해 데미지가 들어가지 않음");
                }
                else
                {
                    CoverData coverData = GetCoverDataAtPosition(targetMap.ObjectLocation);
                    if (coverData != null)
                    {
                        float damagePercent = coverData.GetDamagePercent();
                        Debug.Log($"데미지가 {damagePercent * 100}% 만큼 들어감");
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
                    foreach (CoverData coverData in createObject.coverDataArray)
                    {
                        if (coverData.map.ObjectLocation == position)
                        {
                            return coverData;
                        }
                    }
                }
            }
        }
        return null;
    }
}
