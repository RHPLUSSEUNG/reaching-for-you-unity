using UnityEngine;
using System.Collections.Generic;

public enum EObstacle
{
    Wall,
    NotWall,
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
    public GameObject notWallPrefab;

    public float frequency = 0;

    public int minWallCount = 1; // 최소 벽 개수
    public int maxWallCount = 5; // 최대 벽 개수
    public LayerMask wallLayerMask; // Wall 레이어 마스크

    private List<Vector2Int> wallPositions = new List<Vector2Int>(); // 생성된 벽의 위치 리스트

    void Start()
    {
        PlaceCubes();
        // PlaceWalls();

        // Init(Width, Height, 1);
    }

    public void Init(float xSize, float zSize, float cellSize)
    {
        map = new Map[(int)xSize, (int)zSize];

        float xRandom = Random.Range(0, 100f);
        float zRandom = Random.Range(0, 100f);

        for(int x = 0; x < xSize; x++)
        {
            for(int z = 0; z < zSize; z++) 
            {
                map[x, z].ObjectLocation = new Vector3(cellSize * x, 0, cellSize * z);

                float xFloat = x;
                float zFloat = z;
                float xSizeFloat = xSize;
                float zSizeFloat = zSize;
                float gridHeight = Mathf.PerlinNoise(xFloat / xSizeFloat * frequency + xRandom, zFloat / zSizeFloat * frequency + zRandom);

                Debug.Log(gridHeight);

                if (gridHeight > 0.36f && gridHeight < 0.45f)
                {
                    map[x, z].eObstacle = EObstacle.NotWall;
                }
                else if (gridHeight >= 0.45f)
                {
                    map[x, z].eObstacle = EObstacle.Wall;
                }
                else 
                {
                   map[x, z].eObstacle = EObstacle.Ground;
                }
                

                switch (map[x, z].eObstacle)
                {
                    case EObstacle.Wall:
                        map[x, z].ObjectPrefab = Instantiate(wallPrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                        map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, gridHeight, 0);
                        map[x,z].CanWalk = false;
                        break;
                    case EObstacle.NotWall:
                        map[x, z].ObjectPrefab = Instantiate(notWallPrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                        map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, gridHeight + 1f, 0);
                        map[x,z].CanWalk = false;
                        break;
                    case EObstacle.Ground:
                    map[x, z].ObjectPrefab = Instantiate(cubePrefab, map[x, z].ObjectLocation, Quaternion.identity, this.transform);
                        map[x, z].ObjectPrefab.transform.position = map[x, z].ObjectLocation + new Vector3(0, 0f, 0);
                        map[x,z].CanWalk = true;
                        break;      
                 }
             }
        }
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

    void PlaceWalls()
    {
        int wallCount = Random.Range(minWallCount, maxWallCount + 1); // 랜덤한 벽 개수 생성

        for (int i = 0; i < wallCount; i++)
        {
            Vector2Int position = Vector2Int.zero; // 벽의 위치를 초기화

            // 큐브 내부에 있는 벽을 찾을 때까지 반복
            while (true)
            {
                // 랜덤한 위치에 벽 생성
                int randomX = Random.Range(0, (int)Width);
                int randomY = Random.Range(0, (int)Height);
                position = new Vector2Int(randomX, randomY);

                // 벽의 위치가 이미 다른 벽과 겹치는지 확인
                if (!wallPositions.Contains(position))
                {
                    wallPositions.Add(position);
                    break;
                }
            }

            Quaternion rotation = Quaternion.Euler(0f, Random.Range(0, 4) * 90f, 0f); // 0, 90, 180, 270 중에서 랜덤한 각도 선택

            // 새로운 Wall 오브젝트 생성
            GameObject newWall = Instantiate(wallPrefab, new Vector3(position.x, 0f, position.y), rotation);
            // 부모 설정 (이 스크립트를 추가한 게임 오브젝트를 부모로 설정)
            newWall.transform.SetParent(transform);

            // 벽이 있는 위치의 큐브에 ObjectInteraction 스크립트가 있다면 비활성화
            Collider[] colliders = Physics.OverlapBox(newWall.transform.position, Vector3.one * 0.5f, Quaternion.identity, wallLayerMask);
            foreach (Collider collider in colliders)
            {
                ObjectInteraction interaction = collider.GetComponent<ObjectInteraction>();
                if (interaction != null)
                {
                    interaction.enabled = false;
                }
            }
        }
    }
}
