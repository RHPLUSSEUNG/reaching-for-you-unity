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
    public Map[,] map;

    public float Width = 1f; // 가로 길이
    public float Height = 1f; // 세로 길이

    [Header("Prefabs")]
    public GameObject cubePrefab; // 배치할 Cube 프리팹
    
    public GameObject wallPrefab; // 배치할 Wall 프리팹
    public CoverData[] coverDataArray; // 엄폐물 데이터 배열

    public float frequency = 0;

    public int minWallCount = 1; // 최소 벽 개수
    public int maxWallCount = 5; // 최대 벽 개수
    public LayerMask wallLayerMask; // Wall 레이어 마스크

    private List<Vector2Int> wallPositions = new List<Vector2Int>(); // 생성된 벽의 위치 리스트

    void Start()
    {
        PlaceCubes();
        // PlaceWalls();

        RandomObstacle(Width, Height, 1);
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


                if (gridHeight > 2.6f && gridHeight < 5.6f)
                {
                    map[x, z].eObstacle = EObstacle.Obstacle;
                }
                else if (gridHeight >= 5.6f)
                {
                    map[x, z].eObstacle = EObstacle.Wall;
                    Debug.Log(IsObstacleAround(x, z));
                }
                else if (gridHeight <= 2.6f)
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
                        }
                        
                        break;
                    case EObstacle.Obstacle:
                        // 랜덤한 인덱스 생성
                        int randomIndex = Random.Range(0, coverDataArray.Length);
                        // 선택된 인덱스에 해당하는 coverData 사용
                        CoverData selectedCoverData = coverDataArray[randomIndex];
                        // 선택된 coverData에 해당하는 cube 생성
                        map[x, z].ObjectPrefab = Instantiate(selectedCoverData.coverGameObject, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                        map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, coverDataArray[randomIndex].coverGameObject.transform.position.y, 0);
                        map[x, z].CanWalk = false;
                        break;
                }
            }
        }
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
