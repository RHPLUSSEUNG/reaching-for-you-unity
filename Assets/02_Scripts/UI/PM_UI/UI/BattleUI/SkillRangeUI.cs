using System.Collections;
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

            HighlightRange(current);

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

                if (!visited.ContainsKey(near) && currentDistance + 1 <= skillRange)
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

    public void ClearSkillRangeUI()
    {
        List<Vector3Int> keyList = new List<Vector3Int>();

        foreach (Vector3Int pos in visited.Keys)
        {
            GameObject tile = battleMap[pos.x, pos.z];
            tile.GetComponent<Renderer>().material.color = originalColor;

            keyList.Add(pos);
        }

        foreach (Vector3Int key in keyList)
        {
            visited.Remove(key);
        }
    }

}
