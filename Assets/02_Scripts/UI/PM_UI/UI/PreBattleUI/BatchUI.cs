using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BatchUI : UI_Popup
{
    enum batchUI
    {
        PlayerSpawn,
        CancleButton,
        FinishButton
    }

    public GameObject preBattleUI;

    public GameObject playerSpawn;
    public GameObject cancleBtn;
    public GameObject finishBtn;

    GameObject[,] battleMap;
    List<GameObject> obstacleMap = new List<GameObject>();
    float mapWidth;
    float mapHeight;
    List<Color> tileColor = new List<Color>();
    Color ableColor = new Color(96f / 255f, 96f / 255f, 255f / 255f, 1f);
    Color disabledColor = new Color(236f / 255f, 80f / 255f, 90f / 255f, 1f);

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(batchUI));

        playerSpawn = GetObject((int)batchUI.PlayerSpawn);
        cancleBtn = GetObject((int)batchUI.CancleButton);
        finishBtn = GetObject((int)batchUI.FinishButton);

        BindEvent(playerSpawn, PlayerSpawn, Define.UIEvent.Click);
        BindEvent(cancleBtn, CancleButtonClick, Define.UIEvent.Click);
        BindEvent(finishBtn, FinishButtonClick, Define.UIEvent.Click);

        Managers.UI.ShowUI(playerSpawn);
        Managers.UI.ShowUI(finishBtn);
        Managers.UI.HideUI(cancleBtn);
    }

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

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                tile = map.transform.GetChild(tileCount).gameObject;
                bool spawnCheck = mapInfo.IsEnemySpawnPosition((int)tile.transform.position.x, (int)tile.transform.position.z);
                if (!spawnCheck)
                {
                    // 플레이어 생성 구역
                    tileColor.Add(tile.GetComponent<Renderer>().material.color);
                    tile.GetComponent<Renderer>().material.color = ableColor;
                }
                else
                {
                    // 몬스터 생성 구역
                    tileColor.Add(tile.GetComponent<Renderer>().material.color);
                    tile.GetComponent<Renderer>().material.color = disabledColor;
                }
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
                int tileX = (int)obstacle.transform.position.x;
                int tileZ = (int)obstacle.transform.position.z;
                GameObject obstacleTile = battleMap[tileX, tileZ];
                obstacleTile.GetComponent<Renderer>().material.color = disabledColor;
                obstacleMap.Add(obstacle);
            }
        }
    }

    public void PlayerSpawn(PointerEventData data)
    {
        Debug.Log("Player Spawn");
        Managers.UI.ShowUI(cancleBtn);
        Managers.UI.HideUI(playerSpawn);
        Managers.UI.HideUI(finishBtn);

        // TODO : 생성하는 Player가 2명 이상일 때 수정 필요
        Managers.BattleUI.player = Managers.Party.playerParty[0];
        if (Managers.BattleUI.player == null)
        {
            Debug.Log("Player Null");
        }
        Managers.UI.uiState = UIState.PlayerSet;
    }

    public void CancleButtonClick(PointerEventData data)
    {
        if(Managers.UI.uiState == UIState.PlayerSet)
        {
            Managers.UI.ShowUI(playerSpawn);
            Managers.UI.ShowUI(finishBtn);
            Managers.UI.HideUI(cancleBtn);
            Managers.UI.uiState = UIState.Idle;
        }
    }

    public void FinishButtonClick(PointerEventData data)
    {
        Managers.UI.uiState = UIState.Idle;
        Managers.UI.HideUI(gameObject);
        Managers.UI.ShowUI(preBattleUI);

        int colorCount = 0;
        for(int x = 0; x < mapWidth;x++)
        {
            for (int z = 0; z< mapHeight; z++)
            {
                GameObject tile = battleMap[x, z];
                tile.GetComponent<Renderer>().material.color = tileColor[colorCount];
                colorCount++; ;
            }
        }
        tileColor.Clear();
    }
}
