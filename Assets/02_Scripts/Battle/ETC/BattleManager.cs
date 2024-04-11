using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region singleton
    static BattleManager _instance;
    public static BattleManager Instance { get { return _instance; } }

    public void Init()
    {
        GameObject go = GameObject.Find("Manager");
        if (go == null)
        {
            go = new GameObject { name = "Manager" };
            go.AddComponent<BattleManager>();
        }
        if(go.GetComponent<BattleManager>() == null)
        {
            go.AddComponent<BattleManager>();
        }
        DontDestroyOnLoad(go);
        _instance = go.GetComponent<BattleManager>();
    }
    #endregion
    
    public BattleState battleState;
    public short playerLive;
    public short monsterLive;
    
    public void BattleStart()
    {
        foreach (GameObject character in GameParty.party.playerParty)
        {
            //Generate Character on Map
            Instantiate(character, new Vector3(), new Quaternion());
        }
        foreach (GameObject character in GameParty.party.monsterParty)
        {
            //Generate Character on Map
            Instantiate(character, new Vector3(), new Quaternion());
        }
        playerLive = (short)GameParty.party.playerParty.Count;
        monsterLive = (short)GameParty.party.monsterParty.Count;
        battleState = BattleState.PlayerTurn;
    }

    public void NextTurn()
    {
        List<GameObject> party;
        if (battleState == BattleState.PlayerTurn)
        {
            party = GameParty.party.playerParty;
            battleState = BattleState.EnemyTurn;
        }
        else
        {
            party = GameParty.party.monsterParty;
            battleState = BattleState.PlayerTurn;

        }
        foreach (GameObject g in party)
        {
            CharacterSpec spec = g.GetComponent<CharacterSpec>();
            foreach(Buff buff in spec.buffs)
            {
                buff.TimeCheck();
            }
            foreach (Debuff debuff in spec.debuffs)
            {
                debuff.TimeCheck();
            }
            spec.remainStamina = spec.stamina;
        }
    }

    public void Result()
    {
        if (playerLive == 0)
        {
            battleState = BattleState.Victory;
        }
        else if (monsterLive == 0)
        {
            battleState = BattleState.Defeat;
        }
        else return;
    }
}
