using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager
{
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    public List<GameObject> ObjectList = new();
    public List<GameObject> InstantAfterPhaseList = new();
    public GameObject currentCharacter;

    public int totalTurnCnt = 0;
    public bool isPlayerTurn = false;
    int turnCnt = 0;
    int phase = 1;
    public List<GameObject> Areas = new();

    public CameraController cameraController;

    private int compareDefense(GameObject character1, GameObject character2)
    {
        return character1.GetComponent<EntityStat>().Defense < character2.GetComponent<EntityStat>().Defense ? -1 : 1;
    }

    public bool Can_Continue()
    {
        if (currentCharacter == null)
        {
            return true;
        }
        else if (battleState == BattleState.Defeat && battleState == BattleState.Victory)
        {
            Debug.Log("Battle is End");
            return false;
        }
        else if (Managers.Skill.is_effect)
        {
            Debug.Log("Wait for skill effect End");
            return false;
        }
        //else if (currentCharacter.GetComponent<PlayerBattle>().isMoving)
        //{
        //    Debug.Log("Wait for Character Moving");
        //    return false;
        //}
        else
        {
            return true;
        }   
    }

    public void BattleReady()
    {
        cameraController = GameObject.Find("Main Camera").GetComponent<CameraController>();

        ObjectList.Clear();
        battleState = BattleState.Start;
        phase = 1;
    }

    public void BattleStart()
    {
        if ((playerLive == 0))
        {
            Result();
        }
        battleState = BattleState.PlayerTurn;
        Managers.Skill.ReadyGameSkill();
        ObjectList.Sort(compareDefense);

        Managers.BattleUI.turnUI.InstantiateAllTurnOrderUI();
        Managers.BattleUI.turnUI.UpdateTurnUI(turnCnt);

        NextTurn();
    }

    public void CalcTurn()
    {
        if (currentCharacter != null && currentCharacter.GetComponent<CharacterState>() != null)
        {
            currentCharacter.GetComponent<CharacterState>().CalcTurn();
        }

        turnCnt++;
        if (turnCnt >= ObjectList.Count)
        {
            phase++;
            Managers.BattleUI.turnUI.ResetPastPanel();
            turnCnt = 0;


            for (int i = 0; i < InstantAfterPhaseList.Count; i++)
            {
                ObjectList.Add(InstantAfterPhaseList[i]);
            }
            InstantAfterPhaseList.Clear();

            ObjectList.Sort(compareDefense);
            Managers.BattleUI.turnUI.UpdateTurnUI(turnCnt);
        }
    }
    public bool CheckGameEnd()
    {
        if (playerLive == 0 || monsterLive == 0)
        {
            return true;
        }
        return false;
    }
    public void PlayerTurn()
    {
        //Camera Movement
        Debug.Log("Camera Set");

        battleState = BattleState.PlayerTurn;
        totalTurnCnt++;
        isPlayerTurn = true;
        currentCharacter.GetComponent<PlayerBattle>().OnTurnStart();
        Managers.BattleUI.actUI.UpdateCharacterInfo();
        Managers.BattleUI.turnUI.HideTurnOrderUI();
    }
    public void EnemyTurn()
    {
        //Debug.Log(character.name);
        if (currentCharacter.GetComponent<EnemyAI_Base>() == null)
        {
            Debug.Log("Component Error");
            return;
        }
        battleState = BattleState.EnemyTurn;
        isPlayerTurn = false;

        currentCharacter.GetComponent<EnemyAI_Base>().ProceedTurn();
    }

    public void NextTurn()
    {
        Managers.Manager.StartCoroutine(NextTurnCoroutine());
    }

    public void Result()
    {
        if (monsterLive == 0)
        {
            battleState = BattleState.Victory;
            Debug.Log("Victory");
        }
        else if (playerLive == 0)
        {
            battleState = BattleState.Defeat;
            Debug.Log("Defeat");
        }
        //CameraAllocate(null);
        //Battle End Manager Object reset
        BattleClear();
        Managers.Party.ClearParty();
        Managers.Skill.SkillClear();
        Managers.raycast.RayClear();
        SceneChanger.Instance.ChangeScene(SceneType.PM_ADVENTURE);
    }

    public IEnumerator NextTurnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        //wait for animation end
        while (!Can_Continue())
        {
            yield return null; //돌아오는 장소
        }
        //calculate turn
        if (CheckGameEnd())
        {
            Result();
            yield break;
        }
        currentCharacter = ObjectList[turnCnt];
        Debug.Log($"Turn : {currentCharacter}");
        CalcTurn();
        //camera setting
        cameraController.ChangeFollowTarget(currentCharacter, true);
        cameraController.ChangeCameraMode(CameraMode.Follow, false, true);
        //cameraController.ChangeOffSet(-3, 1.5f, -3, 20, 45);
        if(!Managers.BattleUI.turnUI.GetState())
        {            
            Managers.BattleUI.turnUI.ShowTurnOrderUI();
        }

        //Calculate Area Remain Turn
        if (Areas.Count != 0)
        {
            foreach (GameObject area in Areas)
            {
                area.GetComponent<AreaInterface>().CalcTurn();
                Debug.Log(area.name);
            }
        }
        
        //Stun cant active turn
        if (currentCharacter.GetComponent<CharacterState>().IsStun())
        {
            NextTurn();
            yield break;
        }

        if (currentCharacter.GetComponent<CharacterState>().IsBind())
        {
            currentCharacter.GetComponent<EntityStat>().MovePoint = 0;
        }

        if (currentCharacter.CompareTag("Player"))
        {
            Managers.BattleUI.battleUI.StartCoroutine(Managers.BattleUI.battleUI.StartSlide("Player Turn!"));
        }
        else
        {
            Managers.BattleUI.battleUI.StartCoroutine(Managers.BattleUI.battleUI.StartSlide("Enemy Turn!"));
        }
        Managers.BattleUI.turnUI.ProceedTurnUI(turnCnt);
        yield break;
    }

    public void BattleClear()
    {
        currentCharacter = null;
        totalTurnCnt = 0;
        isPlayerTurn = false;
        turnCnt = 0;
        phase = 1;
        Areas.Clear();
        ObjectList.Clear();
}

    #region Camera
    public void CameraAllocate(GameObject target)
    {
        cameraController.ChangeFollowTarget(target, true);
    }
    #endregion
}
