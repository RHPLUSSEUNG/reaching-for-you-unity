using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Battle : UI_Scene
{
    enum BattleUIElement
    {
        PlayerSpawn,
        MonsterSpawn,
        Cancle,
        BattleStart,
        NextTurn,
        MagicPanel
    }

    public GameObject player;
    public ButtonState state = ButtonState.Idle;

    GameObject playerSpawnBtn;
    GameObject MonsterSpawnBtn;
    GameObject cancleBtn;
    GameObject battleStartBtn;
    GameObject nextBtn;
    GameObject magicPanel;

    public override void Init()
    {
        Bind<GameObject>(typeof(BattleUIElement));

        playerSpawnBtn = GetObject((int)BattleUIElement.PlayerSpawn);
        MonsterSpawnBtn = GetObject((int)BattleUIElement.MonsterSpawn);
        cancleBtn = GetObject((int)BattleUIElement.Cancle);
        battleStartBtn = GetObject((int)BattleUIElement.BattleStart);
        nextBtn = GetObject((int)BattleUIElement.NextTurn);
        magicPanel = GetObject((int)BattleUIElement.MagicPanel);

        BindEvent(playerSpawnBtn, PlayerSpawn, Define.UIEvent.Click);
        BindEvent(MonsterSpawnBtn, MonsterSpawn, Define.UIEvent.Click);
        BindEvent(cancleBtn, CancleButtonClick, Define.UIEvent.Click);
        BindEvent(battleStartBtn, BattleStart, Define.UIEvent.Click);
        BindEvent(nextBtn, NextTurn, Define.UIEvent.Click);

        nextBtn.gameObject.SetActive(false);
        magicPanel.SetActive(false);
        Debug.Log($"Magic Panel{magicPanel}");
    }

    public void PlayerSpawn(PointerEventData data)
    {
        Debug.Log("Player Spawn");
        player = Managers.Party.playerParty[0];
        Managers.PlayerButton.player = Managers.Party.playerParty[0];

        state = ButtonState.PlayerSet;
        Managers.PlayerButton.state = ButtonState.PlayerSet;
    }

    public void MonsterSpawn(PointerEventData data)
    {
        Debug.Log("Monster Spawn");
        player = Managers.Party.monsterParty[0];
        Managers.PlayerButton.player = Managers.Party.monsterParty[0];

        state = ButtonState.PlayerSet;
        Managers.PlayerButton.state = ButtonState.PlayerSet;
    }
    
    public void CancleButtonClick(PointerEventData data)
    {
        Debug.Log("Cancle Button");
        state = ButtonState.Idle;
        Managers.PlayerButton.state = ButtonState.PlayerSet;
    }

    public void BattleStart(PointerEventData data)
    {
        Debug.Log("Battle start");
        if(Managers.Battle.battleState == BattleState.Start)
        {
            Managers.Battle.BattleStart();
            playerSpawnBtn.gameObject.SetActive(false);
            MonsterSpawnBtn.gameObject.SetActive(false);
            cancleBtn.gameObject.SetActive(false);
            battleStartBtn.gameObject.SetActive(false);
            nextBtn.gameObject.SetActive(true);
        }
    }

    public void SetPosition(GameObject pos)
    {
        player.transform.position = pos.transform.position + Vector3.up;
        state = ButtonState.Idle;
        Managers.PlayerButton.state = ButtonState.Idle;
    }

    public void NextTurn(PointerEventData data)
    {
        if(Managers.Battle.battleState == BattleState.PlayerTurn)
        {
            Managers.Battle.NextTurn();
            Debug.Log("Next Turn");
        }
    }

    public Active GetSkill()
    {   
        UI_MagicPanel magic = magicPanel.GetComponent<UI_MagicPanel>();
        Active skill = magic.GetSkill();
        //Debug.Log($"Skill : {skill}");

        return skill;
    }
}
