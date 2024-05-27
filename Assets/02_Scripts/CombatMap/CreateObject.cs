using UnityEngine;
using System.Collections.Generic;

public enum EObstacle
{
    Wall,
    Obstacle,
    Ground,
}

public struct Map
{
    public GameObject ObjectPrefab;
    public Vector3 ObjectLocation;
    public EObstacle eObstacle;
    public bool CanWalk;
}

public class CreateObject : MonoBehaviour
{
    public GameObject manager;  // 맵 생성 후 활성화할 매니저
    public Map[,] map;

    public float Width = 1f; // 가로 길이
    public float Height = 1f; // 세로 길이

    [Header("Prefabs")]
    public GameObject cubePrefab; // 배치할 Cube 프리팹
    
    public GameObject wallPrefab; // 배치할 Wall 프리팹
    public CoverData[] coverDataArray; // 엄폐물 데이터 배열

    public float frequency = 0;

    public int minObstacleCount = 1; // 최소 벽 개수
    public int maxObstacleCount = 5; // 최대 벽 개수
    public float minDistanceBetweenObstacles = 2.0f; // 장애물 간 최소 거리

    int currentObstacleCount = 0;

    public LayerMask wallLayerMask; // Wall 레이어 마스크

    private List<Vector2Int> wallPositions = new List<Vector2Int>(); // 생성된 벽의 위치 리스트
    private List<Vector2Int> obstaclePositions = new List<Vector2Int>(); // 생성된 장애물의 위치 리스트

    void Start()
    {
        PlaceCubes();
        RandomObstacle(Width, Height, 1);
        PlaceObstacles();

        manager.active = true;
    }

    public void RandomObstacle(float Width, float Height, int cellSize) 
    {
        map = new Map[(int)Width, (int)Height];

         float xRandom = Random.Range(0f, 100f);
         float zRandom = Random.Range(0f, 100f);

         for (int x = 0; x < Width; x++)
         {
             for (int z = 0; z < Height; z++)
             {
                map[x, z].ObjectLocation = new Vector3(cellSize * x, 0, cellSize * z);

                float xFloat = x;
                float zFloat = z;
                float xSizeFloat = Width;
                float zSizeFloat = Height;
                float gridHeight = Mathf.PerlinNoise(xFloat / xSizeFloat * frequency + xRandom, zFloat / zSizeFloat * frequency + zRandom) * 10;


                if (gridHeight > 3.3f && gridHeight < 5.5f)
                {
                    map[x, z].eObstacle = EObstacle.Obstacle;
                }
                else if (gridHeight >= 5.5f)
                {
                    map[x, z].eObstacle = EObstacle.Wall;
                    Debug.Log(IsObstacleAround(x, z));
                }
                else if (gridHeight <= 3.3f)
                {
                    // Debug.Log(gridHeight);

                    map[x, z].eObstacle = EObstacle.Ground;
                }

                switch (map[x, z].eObstacle)
                {
                    case EObstacle.Ground:
                        // ground는 생성 ㄴㄴ
                        break;
                    case EObstacle.Wall:
                        if(IsObstacleAround(x, z) <= 3)
                        {
                            map[x, z].ObjectPrefab = Instantiate(wallPrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                            map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, 1f, 0);
                            map[x,z].CanWalk = false;
                            wallPositions.Add(new Vector2Int(x, z));
                        }
                        break;
                    case EObstacle.Obstacle:
                        // 랜덤한 인덱스 생성
                        int randomIndex = Random.Range(0, coverDataArray.Length);
                        // 선택된 인덱스에 해당하는 coverData 사용
                        CoverData selectedCoverData = coverDataArray[randomIndex];
                        // 선택된 coverData에 해당하는 cube 생성
                        // if(maxObstacleCount > currentObstacleCount) {
                        //     map[x, z].ObjectPrefab = Instantiate(selectedCoverData.coverGameObject, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                        //     map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, coverDataArray[randomIndex].coverGameObject.transform.position.y, 0);
                        //     map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, 1f, 0);
                        //     map[x, z].CanWalk = false;
                        //     currentObstacleCount++;
                        // }
                        
                        break;
                }
            }
        }
    }

    public void PlaceObstacles()
    {
        int obstacleCount = Random.Range(minObstacleCount, maxObstacleCount + 1);
        while (currentObstacleCount < obstacleCount)
        {
            int x = Random.Range(0, (int)Width);
            int z = Random.Range(0, (int)Height);

            if (map[x, z].eObstacle == EObstacle.Obstacle && IsPositionFarFromWalls(x, z))
            {
                // 랜덤한 인덱스 생성
                int randomIndex = Random.Range(0, coverDataArray.Length);
                CoverData selectedCoverData = coverDataArray[randomIndex];

                map[x, z].eObstacle = EObstacle.Obstacle;
                map[x, z].ObjectPrefab = Instantiate(selectedCoverData.coverGameObject, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, coverDataArray[randomIndex].coverGameObject.transform.position.y, 0);
                map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, 0.5f, 0);
                Vector3 newRotation = new Vector3(-90f, 0, 0); // 원하는 각도로 변경
                map[x, z].ObjectPrefab.transform.rotation = Quaternion.Euler(newRotation);
                obstaclePositions.Add(new Vector2Int(x, z));
                currentObstacleCount++;
            }
        }
    }

    private bool IsPositionFarFromWalls(int x, int z)
    {
        foreach (Vector2Int wallPos in wallPositions)
        {
            if (Vector2Int.Distance(new Vector2Int(x, z), wallPos) < minDistanceBetweenObstacles)
            {
                return false;
            }
        }
        return true;
    }

    public Map[,] GetMap()
    {
        return map;
    }


    public int IsObstacleAround(int x, int z)
    {
        int count = 0;
        // 왼쪽 확인
        if (x > 0 && map[x - 1, z].eObstacle == EObstacle.Wall)
            count++;

        // 오른쪽 확인
        if (x < Width - 1 && map[x + 1, z].eObstacle == EObstacle.Wall)
            count++;

        // 위쪽 확인
        if (z < Height - 1 && map[x, z + 1].eObstacle == EObstacle.Wall)
            count++;

        // 아래쪽 확인
        if (z > 0 && map[x, z - 1].eObstacle == EObstacle.Wall)
            count++;

        return count;
    }


    void PlaceCubes()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                // 새로운 Cube 오브젝트 생성
                GameObject newCube = Instantiate(cubePrefab, new Vector3(x, 0, y), Quaternion.identity);
                // 부모 설정 (이 스크립트를 추가한 게임 오브젝트를 부모로 설정)
                newCube.transform.SetParent(transform);
            }
        }
    }
}
