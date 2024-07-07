using System.Collections.Generic;
using UnityEngine;

public class SkillRangeUI : MonoBehaviour
{
    GameObject[,] battleMap;

    float mapWidth;
    float mapHeight;
    Color highlightColor = new Color(236f / 255f, 80f / 255f, 90f / 255f, 1f);
    Color originalColor;

    Dictionary<Vector3Int, int> visited;
    List<GameObject> tiles = new List<GameObject>();
    List<Color> tileColor = new List<Color>();

    public void SetMapInfo()
    {
        // MoveRaneUI와 동일한 작업을 하므로 통합
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
    }

    public void DisplaySkillRange()
    {
        GameObject focusPlayer = Managers.Battle.currentCharacter;
        SkillData skillData = Managers.Data.GetPlayerSkillData(Managers.BattleUI.skill_ID);
        int skillRange = skillData.range;

        PathFinder.RequestSkillRange(focusPlayer.transform.position, skillRange, RangeType.Normal, HighlightRange);
    }

    public void HighlightRange(List<GameObject> tileList)
    {
        foreach(GameObject tile in tileList)
        {
            tileColor.Add(tile.GetComponent<Renderer>().material.color);
            tile.GetComponent<Renderer>().material.color = highlightColor;
            tiles.Add(tile);
        }
    }
    
    public void ClearSkillRange()
    {
        int colorCount = 0;
        foreach(GameObject tile in tiles)
        {
            tile.GetComponent<Renderer>().material.color = tileColor[colorCount];
            colorCount++;
       }
        tiles.Clear();
        tileColor.Clear();
    }
}
