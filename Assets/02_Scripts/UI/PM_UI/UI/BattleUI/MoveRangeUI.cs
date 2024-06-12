using System.Collections.Generic;
using UnityEngine;

public class MoveRangeUI : MonoBehaviour
{
    GameObject[,] battleMap;
    int moveCost = 10;
    float mapWidth;
    float mapHeight;
    Color highlightColor = Color.cyan;

    public void SetMapInfo()
    {
        GameObject map = GameObject.Find("MapSpawner");
        CreateObject mapInfo = map.GetComponent<CreateObject>();
        mapWidth = mapInfo.Width;
        mapHeight = mapInfo.Height;
        battleMap = new GameObject[(int)mapWidth, (int)mapHeight];

        // Tile 정보 저장
        GameObject tile;
        int tileCount = 0;
        for(int x = 0; x < mapWidth; x++)
        {
            for(int z = 0; z < mapHeight; z++)
            {
                tile = map.transform.GetChild(tileCount).gameObject;
                battleMap[x, z] = tile;
                tileCount++;
            }
        }
    }

    public void DisplayMoveRange()
    {
        GameObject focusPlayer = Managers.Battle.currentCharacter;

        int maxMoveRange = focusPlayer.GetComponent<EntityStat>().MovePoint / moveCost;

        float currentPosX = focusPlayer.transform.position.x;
        float currentPosZ = focusPlayer.transform.position.z;

        Vector3Int currentPos = new((int)currentPosX, 0, (int)currentPosZ);

        Queue<Vector3Int> moveTile = new Queue<Vector3Int>();
        Dictionary<Vector3Int, int> visited = new Dictionary<Vector3Int, int>();

        moveTile.Enqueue(currentPos);
        visited[currentPos] = 0;

        while (moveTile.Count > 0)
        {
            Vector3Int current = moveTile.Dequeue();
            int currentDistance = visited[current];

            HighlightRange(current);     // 타일 색 변경

            Vector3Int[] directions =
            {
                new Vector3Int(1,0,0), new Vector3Int(-1,0,0), new Vector3Int(0,0,1), new Vector3Int(0, 0, -1),
                new Vector3Int(1, 0, 1), new Vector3Int(1, 0, -1), new Vector3Int(-1, 0, 1), new Vector3Int(-1, 0, -1)
            };

            foreach (Vector3Int direction in directions)
            {
                Vector3Int near = current + direction;
                if (near.x < 0 || near.z < 0)   // 맵을 벗어남
                {
                    continue;
                }

                if (!visited.ContainsKey(near) && currentDistance + 1 <= maxMoveRange)
                {
                    moveTile.Enqueue(near);
                    visited[near] = currentDistance + 1;
                }
            }
        }
    }

    void HighlightRange(Vector3Int pos)
    {
        int x = pos.x;
        int z = pos.z;
        GameObject tile = battleMap[x, z];
        tile.GetComponent<Renderer>().material.color = highlightColor;
    }
}
