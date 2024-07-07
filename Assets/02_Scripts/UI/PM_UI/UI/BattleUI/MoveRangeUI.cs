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
        movePoint = movePoint / 10;

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

    public void ShowPathRange(List<Vector3> path)
    {
        ClearMoveRange();
        int x, z;
        for (int i = 0; i < path.Count; i++)
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
}
