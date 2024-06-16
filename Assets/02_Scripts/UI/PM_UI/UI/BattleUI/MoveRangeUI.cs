using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveRangeUI : MonoBehaviour
{
    GameObject[,] battleMap;
    List<GameObject> obstacleMap = new List<GameObject>();

    int moveCost = 10;
    float mapWidth;
    float mapHeight;
    Color highlightColor = Color.cyan;
    Color originalColor;

    Dictionary<Vector3Int, int> visited;

    public void SetMapInfo()
    {
        GameObject map = GameObject.Find("MapSpawner");
        CreateObject mapInfo = map.GetComponent<CreateObject>();
        mapWidth = mapInfo.Width;
        mapHeight = mapInfo.Height;
        battleMap = new GameObject[(int)mapWidth, (int)mapHeight];

        // Tile ���� ����
        GameObject tile;
        int tileCount = 0;

        originalColor = map.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;

        for(int x = 0; x < mapWidth; x++)
        {
            for(int z = 0; z < mapHeight; z++)
            {
                tile = map.transform.GetChild(tileCount).gameObject;
                battleMap[x, z] = tile;
                tileCount++;
            }
        }

        // ��ֹ� ���� ����(����)
        for(int i = tileCount; i < map.transform.childCount; i++)
        {
            if (map.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("EnvironmentObject"))
            {
                GameObject obstacle = map.transform.GetChild(i).gameObject;
                obstacleMap.Add(obstacle);
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
        visited = new Dictionary<Vector3Int, int>();

        moveTile.Enqueue(currentPos);
        visited[currentPos] = 0;

        while (moveTile.Count > 0)
        {
            Vector3Int current = moveTile.Dequeue();
            int currentDistance = visited[current];

            HighlightRange(current);     // Ÿ�� �� ����

            Vector3Int[] directions =
            {
                new Vector3Int(1,0,0), new Vector3Int(-1,0,0), new Vector3Int(0,0,1), new Vector3Int(0, 0, -1),
                new Vector3Int(1, 0, 1), new Vector3Int(1, 0, -1), new Vector3Int(-1, 0, 1), new Vector3Int(-1, 0, -1)
            };

            foreach (Vector3Int direction in directions)
            {
                Vector3Int near = current + direction;
                if (near.x < 0 || near.z < 0 || near.x >= mapWidth || near.z >= mapHeight)   // ���� ���
                {
                    continue;
                }

                if (CheckMovable(near, current))
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

    public void ClearMoveRangeUI()
    {
        List<Vector3Int> keyList = new List<Vector3Int>();

        foreach(Vector3Int pos in visited.Keys)
        {
            GameObject tile = battleMap[pos.x, pos.z];
            tile.GetComponent<Renderer>().material.color = originalColor;

            keyList.Add(pos);
        }

        foreach(Vector3Int key in keyList)
        {
            visited.Remove(key);
        }
    }

    void HighlightRange(Vector3Int pos)
    {
        int x = pos.x;
        int z = pos.z;
        GameObject tile = battleMap[x, z];
        tile.GetComponent<Renderer>().material.color = highlightColor;
    }

    bool CheckMovable(Vector3Int near, Vector3Int current)
    {
        for(int i = 0; i < obstacleMap.Count; i++)
        {
            int x = (int)obstacleMap[i].transform.position.x;
            int z = (int)obstacleMap[i].transform.position.z;
            if(near.x == x && near.z == z)
            {
                return true;        // ��ֹ��� ����
            }
        }

        for(int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            int x = (int)Managers.Battle.ObjectList[i].transform.position.x;
            int z = (int)Managers.Battle.ObjectList[i].transform.position.z;
            if (near.x == x && near.z == z)
            {
                return true;        // ĳ���Ͱ� ����
            }
        }

        if (Mathf.Abs(near.x - current.x) == 1 && Mathf.Abs(near.z - current.z) == 1)
        {
            Vector3Int horizontalCheck = new Vector3Int(near.x, 0, current.z);
            Vector3Int verticalCheck = new Vector3Int(current.x, 0, near.z);

            for (int i = 0; i < obstacleMap.Count; i++)
            {
                int x = (int)obstacleMap[i].transform.position.x;
                int z = (int)obstacleMap[i].transform.position.z;
                if ((horizontalCheck.x == x && horizontalCheck.z == z) || (verticalCheck.x == x && verticalCheck.z == z))
                {
                    return true; // �밢�� �̵� �Ұ�
                }
            }

            for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
            {
                int x = (int)Managers.Battle.ObjectList[i].transform.position.x;
                int z = (int)Managers.Battle.ObjectList[i].transform.position.z;
                if ((horizontalCheck.x == x && horizontalCheck.z == z) || (verticalCheck.x == x && verticalCheck.z == z))
                {
                    return true; // �밢�� �̵� �Ұ�
                }
            }
        }

        return false;
    }
}
