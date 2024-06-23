using System.Collections.Generic;
using UnityEngine;

public class MoveRangeUI : MonoBehaviour
{
    GameObject[,] battleMap;
    List<GameObject> obstacleMap = new List<GameObject>();

    int moveCost = 10;
    float mapWidth;
    float mapHeight;
    Color highlightColor = Color.cyan;
    Color pathColor = Color.yellow;
    Color originalColor;

    List<GameObject> tiles = new List<GameObject>();
    List<GameObject> pathTiles = new List<GameObject>();
    List<Color> tileColor = new List<Color>();
    Dictionary<Vector3Int, int> visited;

    public void SetMapInfo()
    {
        GameObject mapSpawner = GameObject.Find("MapSpawner");
        GameObject map = Util.FindChild(mapSpawner, "Map", false);
        CreateObject mapInfo = mapSpawner.GetComponent<CreateObject>();
        mapWidth = mapInfo.Width;
        mapHeight = mapInfo.Height;
        battleMap = new GameObject[(int)mapWidth, (int)mapHeight];

        // Tile 정보 저장
        GameObject tile;
        int tileCount = 0;

        originalColor = map.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                tile = map.transform.GetChild(tileCount).gameObject;
                battleMap[x, z] = tile;
                tileCount++;
            }
        }

        // 장애물 정보 저장(임의)
        for (int i = tileCount; i < map.transform.childCount; i++)
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
        int movePoint = focusPlayer.GetComponent<EntityStat>().MovePoint;

        PathFinder.RequestSkillRange(focusPlayer.transform.position, movePoint, RangeType.Move, HighlightRange);
    }

    public void HighlightRange(List<GameObject> tileList)
    {
        foreach (GameObject tile in tileList)
        {
            bool hoverCheck = tile.GetComponent<MouseHover>().isHovered;
            if (hoverCheck)
            {
                tileColor.Add(tile.GetComponent<MouseHover>().originalColor);
                tile.GetComponent<MouseHover>().originalColor = highlightColor;
                tile.GetComponent<Renderer>().material.color = highlightColor;
                tiles.Add(tile);
                continue;
            }
            tileColor.Add(tile.GetComponent<Renderer>().material.color);
            tile.GetComponent<Renderer>().material.color = highlightColor;
            tiles.Add(tile);
        }
    }

    public void ClearMoveRange()
    {
        int colorCount = 0;
        foreach (GameObject tile in tiles)
        {
            tile.GetComponent<Renderer>().material.color = tileColor[colorCount];
            colorCount++;
        }

        tiles.Clear();
        tileColor.Clear();
    }

    public void ShowPathRange(Vector3[] path)
    {
        PrevClearMoveRange();
        int x, z;
        for (int i = 0; i < path.Length; i++)
        {
            x = (int)path[i].x;
            z = (int)path[i].z;
            GameObject tile = battleMap[x, z];
            tileColor.Add(tile.GetComponent<Renderer>().material.color);
            tile.GetComponent<Renderer>().material.color = pathColor;            

            pathTiles.Add(tile);
        }
    }

    public void ClearPathRange()
    {
        int colorCount = 0;
        bool hoverCheck;
        foreach (GameObject tile in pathTiles)
        {
            hoverCheck = tile.GetComponent<MouseHover>().isHovered;
            if(hoverCheck)
            {
                tile.GetComponent<MouseHover>().originalColor = tileColor[colorCount];
            }
            tile.GetComponent<Renderer>().material.color = tileColor[colorCount];
            colorCount++;
        }

        pathTiles.Clear();
        tileColor.Clear();
    }

    public void PrevClearMoveRange()
    {
        int colorCount = 0;
        if (visited.Count == 0)
        {
            return;
        }
        foreach (Vector3Int pos in visited.Keys)
        {
            GameObject tile = battleMap[pos.x, pos.z];
            tile.GetComponent<Renderer>().material.color = tileColor[colorCount];
            colorCount++;
        }

        visited.Clear();
        tileColor.Clear();
    }

    public void PrevDisplayMoveRange()
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

            PrevHighlightRange(current);     // 타일 색 변경

            Vector3Int[] directions =
            {
                new Vector3Int(1,0,0), new Vector3Int(-1,0,0), new Vector3Int(0,0,1), new Vector3Int(0, 0, -1),
                new Vector3Int(1, 0, 1), new Vector3Int(1, 0, -1), new Vector3Int(-1, 0, 1), new Vector3Int(-1, 0, -1)
            };

            foreach (Vector3Int direction in directions)
            {
                Vector3Int near = current + direction;
                if (near.x < 0 || near.z < 0 || near.x >= mapWidth || near.z >= mapHeight)   // 맵을 벗어남
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

    void PrevHighlightRange(Vector3Int pos)
    {
        int x = pos.x;
        int z = pos.z;
        GameObject tile = battleMap[x, z];
        bool hoverCheck = tile.GetComponent<MouseHover>().isHovered;
        if(hoverCheck)
        {
            tileColor.Add(tile.GetComponent<MouseHover>().originalColor);
            tile.GetComponent<MouseHover>().originalColor = highlightColor;
            tile.GetComponent<Renderer>().material.color = highlightColor;
            return;
        }
        tileColor.Add(tile.GetComponent<Renderer>().material.color);
        tile.GetComponent<Renderer>().material.color = highlightColor;
    }

    bool CheckMovable(Vector3Int near, Vector3Int current)
    {
        for (int i = 0; i < obstacleMap.Count; i++)
        {
            int x = (int)obstacleMap[i].transform.position.x;
            int z = (int)obstacleMap[i].transform.position.z;
            if (near.x == x && near.z == z)
            {
                return true;        // 장애물이 있음
            }
        }

        for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
        {
            int x = (int)Managers.Battle.ObjectList[i].transform.position.x;
            int z = (int)Managers.Battle.ObjectList[i].transform.position.z;
            if (near.x == x && near.z == z)
            {
                return true;        // 캐릭터가 있음
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
                    return true; // 대각선 이동 불가
                }
            }

            for (int i = 0; i < Managers.Battle.ObjectList.Count; i++)
            {
                int x = (int)Managers.Battle.ObjectList[i].transform.position.x;
                int z = (int)Managers.Battle.ObjectList[i].transform.position.z;
                if ((horizontalCheck.x == x && horizontalCheck.z == z) || (verticalCheck.x == x && verticalCheck.z == z))
                {
                    return true; // 대각선 이동 불가
                }
            }
        }

        return false;
    }
}
