using UnityEngine;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;

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

public struct Coord
{
    public int X;
    public int Z;

    public Coord(int _x, int _z)
    {
        X = _x;
        Z = _z;
    }

    public static bool operator ==(Coord a, Coord b) 
    {
        return a.X == b.X && a.Z == b.Z;
    }
    public static bool operator !=(Coord a, Coord b) 
    {
        return !(a == b);
    }
}

public class CreateObject : MonoBehaviour
{
    #region properties
    Transform mapHolder; // 해당 위치 아래에 맵, 벽 등 생성
    public GameObject manager;  // 맵 생성 후 활성화할 매니저
    public Map[,] map;
    public List<Coord> allTileCoord;
    public Queue<Coord> shuffleTileCoords;

    bool[,] wallInMap;
    public int seed = 10;
    Coord mapCenter;

    public float Width = 20f; // 가로 길이
    public float Height = 20f; // 세로 길이
    [Range(0, 1)]
    public float outlinePercent; // 맵 테두리 (각 큐브 간 거리)
    [Range(0, 1)]
    public float wallPercent; // 벽이 맵 안에 존재하는 정도

    [Header("Map")]
    public GameObject MapPrefab;

    [Header("Gimmick")]
    public GameObject[] Gimmicks;

    [Header("Prefabs")]
    public GameObject[] cubePrefab; // 배치할 Cube 프리팹
    
    public GameObject wallPrefab; // 배치할 Wall 프리팹
    public CoverData[] coverDataArray; // 엄폐물 데이터 배열

    public float frequency = 0;

    public int minObstacleCount = 1; // 최소 벽 개수
    public int maxObstacleCount = 5; // 최대 벽 개수
    public float minDistanceBetweenObstacles = 2.0f; // 장애물 간 최소 거리

    public int maxGimmickCount = 3; // 최대 기믹 개수
    int currentObstacleCount = 0;

    public LayerMask wallLayerMask; // Wall 레이어 마스크

    #endregion

    void Start()
    {
        GenerateMap();
        // RandomObstacle(Width, Height, 1);
        PlaceObstacles();
        PlaceGimmicks();

        GameObject mapInstance = Instantiate(MapPrefab);
        mapInstance.transform.position = MapPrefab.transform.position;

        manager.active = true;
    }

    public void GenerateMap()
    {
        map = new Map[(int)Width, (int)Height];
        allTileCoord = new List<Coord>();
        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++) 
            {
                allTileCoord.Add(new Coord(i, j));
            }
        }

        int randomSeed = Random.Range(0, 100);
        shuffleTileCoords = new Queue<Coord>(ShuffleArray(allTileCoord.ToArray(), randomSeed)); // 셔플된 좌표의 큐
        mapCenter = new Coord((int)Width / 2, (int)Height / 2);

        string name = "Map";
        if(transform.Find(name)) 
        {
            DestroyImmediate(transform.Find(name).gameObject);
        }

        mapHolder = new GameObject(name).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                // 큐브 랜덤하게 생성
                int randomIndex = Random.Range(0, cubePrefab.Length); 
                // 새로운 Cube 오브젝트 생성

                Vector3 tilePosition = CoordToPosition(x, z);
                GameObject newCube = Instantiate(cubePrefab[randomIndex], tilePosition, Quaternion.identity);
                // 부모 설정 (이 스크립트를 추가한 게임 오브젝트를 부모로 설정)
                newCube.transform.localScale = Vector3.one * (1 - outlinePercent);
                newCube.transform.SetParent(mapHolder);
            }
        }

        wallInMap = new bool[(int)Width, (int)Height];

        int wallCount = (int)(Width * Height * wallPercent);
        int currentObstacleCount = 0;
        for(int i = 0; i < wallCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            wallInMap[randomCoord.X, randomCoord.Z] = true;
            currentObstacleCount++;
            if(randomCoord != mapCenter && MapIsFullyAccessible(wallInMap, currentObstacleCount)) {
                Vector3 wallPosition = CoordToPosition(randomCoord.X, randomCoord.Z) + new Vector3(0, 0.8f, 0);
                GameObject newWall = Instantiate(wallPrefab, wallPosition, Quaternion.identity);
                newWall.transform.SetParent(mapHolder);
            }
            else 
            {
                wallInMap[randomCoord.X, randomCoord.Z] = false;
                currentObstacleCount--;
            }
        }
    }

    bool MapIsFullyAccessible(bool[,] wallInMap, int currentWallCount)
    {
        bool[,] mapFlags = new bool[wallInMap.GetLength(0), wallInMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(mapCenter);
        mapFlags[mapCenter.X, mapCenter.Z] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for(int x = -1; x <= 1; x++)
            {
                for(int z = -1; z <= 1; z++) 
                {
                    int neighborX = tile.X + x;
                    int neighborZ = tile.Z + z;
                    if(x == 0 || z == 0)
                    {
                        if(neighborX >= 0 && neighborX < wallInMap.GetLength(0) && neighborZ >= 0 && neighborZ < wallInMap.GetLength(1)) 
                        {
                            if(!mapFlags[neighborX, neighborZ] && !wallInMap[neighborX, neighborZ])
                            {
                                mapFlags[neighborX, neighborZ] = true;
                                queue.Enqueue(new Coord(neighborX, neighborZ));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(Width * Height - currentWallCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

    Vector3 CoordToPosition(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

    public T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed);

        for(int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length);
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }

    public Coord GetRandomCoord() 
    {
        Coord randomCoord = shuffleTileCoords.Dequeue();
        shuffleTileCoords.Enqueue(randomCoord);

        return randomCoord;
    }

    // 특정 좌표에 벽이 있는지 확인하는 함수
    public bool IsWallAtPosition(int x, int z)
    {
        // 범위를 벗어난 좌표일 경우 벽이 없다고 간주
        if(x < 0 || x >= Width || z < 0 || z >= Height)
        {
            return false;
        }
        return wallInMap[x, z];
    }

    public void PlaceGimmicks()
    {
        int gimmickCount = Random.Range(1, maxGimmickCount); // 0개에서 maxGimmickCoun 개 중에서 랜덤하게 선택
        while(gimmickCount > 0)
        {
            // 기믹을 배치할 랜덤한 위치를 찾음
            int x = Random.Range(0, (int)Width);
            int z = Random.Range(0, (int)Height);

            // 해당 위치가 벽과 충돌하지 않는지, 또는 장애물과 충돌하지 않는지 확인
            if (!IsWallAtPosition(x, z))
            {
                // 기믹을 랜덤하게 선택하여 배치
                int randomIndex = Random.Range(0, Gimmicks.Length);
                GameObject selectedGimmick = Gimmicks[randomIndex];

                // 선택된 기믹을 해당 위치에 배치
                GameObject gimmickInstance = Instantiate(selectedGimmick, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                gimmickInstance.transform.position = map[x, z].ObjectLocation + new Vector3(0, selectedGimmick.transform.position.y, 0);
                gimmickInstance.transform.SetParent(mapHolder);

                // 배치된 기믹의 위치를 리스트에 추가
                gimmickCount--;
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

            if (!IsWallAtPosition(x, z))
            {
                // 랜덤한 인덱스 생성
                int randomIndex = Random.Range(0, coverDataArray.Length);
                CoverData selectedCoverData = coverDataArray[randomIndex];

                Vector3 obstaclePosition = CoordToPosition(x, z) + new Vector3(0, coverDataArray[randomIndex].coverGameObject.transform.position.y + 0.82f, 0);
                GameObject obstacleInstance = Instantiate(selectedCoverData.coverGameObject, obstaclePosition, Quaternion.identity, this.transform);
                obstacleInstance.transform.SetParent(mapHolder);

                // map[x, z].eObstacle = EObstacle.Obstacle;
                // map[x, z].ObjectPrefab = Instantiate(selectedCoverData.coverGameObject, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                // map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, coverDataArray[randomIndex].coverGameObject.transform.position.y, 0);
                // map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, 0.82f, 0);
                // Vector3 newRotation = new Vector3(0f, 0, 0); // 원하는 각도로 변경
                // map[x, z].ObjectPrefab.transform.rotation = Quaternion.Euler(newRotation);
                // obstaclePositions.Add(new Vector2Int(x, z));
                currentObstacleCount++;
            }
        }
    }

    public Map[,] GetMap()
    {
        return map;
    }
}
