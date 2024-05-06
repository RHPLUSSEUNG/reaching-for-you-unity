using UnityEngine;
using System.Collections.Generic;

public class CreateObject : MonoBehaviour
{
    public float Width = 1f; // 가로 길이
    public float Height = 1f; // 세로 길이
    public GameObject cubePrefab; // 배치할 Cube 프리팹
    public GameObject wallPrefab; // 배치할 Wall 프리팹
    public int minWallCount = 1; // 최소 벽 개수
    public int maxWallCount = 5; // 최대 벽 개수
    public LayerMask wallLayerMask; // Wall 레이어 마스크

    private List<Vector2Int> wallPositions = new List<Vector2Int>(); // 생성된 벽의 위치 리스트

    void Start()
    {
        PlaceCubes();
        PlaceWalls();
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
            GameObject newWall = Instantiate(wallPrefab, new Vector3(position.x, 0.5f, position.y), rotation);
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
