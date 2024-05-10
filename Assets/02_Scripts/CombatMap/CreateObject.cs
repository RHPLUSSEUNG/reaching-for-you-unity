using UnityEngine;
using System.Collections.Generic;

public enum EObstacle
{
    Wall,
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
    public CoverData[] coverDataArray; // 엄파물 데이터 배열

    public float frequency = 0;

    public int minWallCount = 1; // 최소 벽 개수
    public int maxWallCount = 5; // 최대 벽 개수
    public LayerMask wallLayerMask; // Wall 레이어 마스크

    private List<Vector2Int> wallPositions = new List<Vector2Int>(); // 생성된 벽의 위치 리스트

    void Start()
    {
        PlaceCubes();
        PlaceWalls();

        // Init(Width, Height, 1);
    }

    // public void Init(float xSize, float zSize, float cellSize)
    // {
    //     map = new Map[(int)xSize, (int)zSize];

    //     float xRandom = Random.Range(0, 100f);
    //     float zRandom = Random.Range(0, 100f);

    //     for(int x = 0; x < xSize; x++)
    //     {
    //         for(int z = 0; z < zSize; z++) 
    //         {
    //             map[x, z].ObjectLocation = new Vector3(cellSize * x, 0, cellSize * z);

    //             float xFloat = x;
    //             float zFloat = z;
    //             float xSizeFloat = xSize;
    //             float zSizeFloat = zSize;
    //             float gridHeight = Mathf.PerlinNoise(xFloat / xSizeFloat * frequency + xRandom, zFloat / zSizeFloat * frequency + zRandom);

    //             Debug.Log(gridHeight);

    //             if (gridHeight > 0.36f && gridHeight < 0.45f)
    //             {
    //                 map[x, z].eObstacle = EObstacle.NotWall;
    //             }
    //             else if (gridHeight >= 0.45f)
    //             {
    //                 map[x, z].eObstacle = EObstacle.Wall;
    //             }
    //             else 
    //             {
    //                map[x, z].eObstacle = EObstacle.Ground;
    //             }
                

    //             switch (map[x, z].eObstacle)
    //             {
    //                 case EObstacle.Wall:
    //                     map[x, z].ObjectPrefab = Instantiate(wallPrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
    //                     map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, gridHeight, 0);
    //                     map[x,z].CanWalk = false;
    //                     break;
    //                 case EObstacle.NotWall:
    //                     map[x, z].ObjectPrefab = Instantiate(notWallPrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
    //                     map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, gridHeight + 1f, 0);
    //                     map[x,z].CanWalk = false;
    //                     break;
    //                 case EObstacle.Ground:
    //                 map[x, z].ObjectPrefab = Instantiate(cubePrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
    //                     map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, 0f, 0);
    //                     map[x,z].CanWalk = true;
    //                     break;      
    //              }
    //          }
    //     }
    // }
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

    void PlaceWalls()
    {
        foreach (CoverData coverData in coverDataArray)
        {
            int randomX = Mathf.RoundToInt(Random.Range(0f, Width)); // X 좌표를 정수로 변환
            int randomZ = Mathf.RoundToInt(Random.Range(0f, Height)); // Z 좌표를 정수로 변환
            Vector3 position = new Vector3(randomX, coverData.coverGameObject.transform.position.y, randomZ); // 정수로 변환된 위치 벡터 생성
            Instantiate(coverData.coverGameObject, position, Quaternion.identity);
            coverData.Init(); // 엄판물의 내구성 초기화
        }
    }
}
