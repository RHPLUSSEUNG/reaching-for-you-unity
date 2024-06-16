using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField]
    private int _stun = 0;
    [SerializeField]
    private int _shackle = 0;
    [SerializeField]
    private int _silence = 0;
    [SerializeField]
    private int _slow = 0;

    #region Attack Type
    [SerializeField]
    public bool closeAttack = false;
    public ElementType AttackType = ElementType.None;
    public bool knock_back = false;
    public bool after_move = false;
    #endregion
    //current buff & debuff
    public List<Buff> buffs = new();
    public List<Debuff> debuffs = new();
    //delete when turn end
    private List<Buff> deleteBuffList = new();
    private List<Debuff> deleteDebuffList = new();
    //create when turn start
    private List<Buff> addBuffList = new();
    private List<Debuff> addDebuffList = new();

    public int capacity = 0; //1 = me 2 = you 3 = all
    public int capacityStack = 0;

    #region SkillState_Electric
    [SerializeField]
    private bool _electricEmmune = false;
    private bool _shock = false;
    public bool can_shock = false;
    public bool can_immune = false;
    
    public void ChangeElecImmune(bool immune)
    {
        _electricEmmune = immune;
    }

    public bool IsElecImmune()
    {
        return _electricEmmune;
    }

    public void ChangeShock(bool shock)
    {
        _shock = shock;
    }

    public bool IsShock()
    {
        return _shock;
    }

    public void CalcCapacity(GameObject target)
    {
        CharacterState currentState = Managers.Battle.currentCharacter.GetComponent<CharacterState>();
        if (currentState.capacity == 1 || currentState.capacity == 3)
        {
            currentState.capacityStack++;
        }
        if (target.GetComponent<CharacterState>().capacity >= 2)
        {
            target.GetComponent<CharacterState>().capacityStack++;
        }

        if(capacityStack >= 15 || can_shock)
        {
            ChangeShock(true);
        }
        else
        {
            ChangeShock(false);
        }
        if(capacityStack >= 20 || can_immune)
        {
            ChangeElecImmune(true);
        }
        else
        {
            ChangeElecImmune(false);
        }
    }

    #endregion

    #region SkillState_Ground
    private bool ghost = false;
    private bool endure = false;
    private int accumulateDmg = 0;

    public void ChangeGhost(bool ghost)
    {
        this.ghost = ghost;
    }

    public bool GetGhost()
    {
        return ghost;
    }

    public void ChangeEndure(bool endure)
    {
        this.endure = endure;
    }

    public bool GetEndure()
    {
        return endure;
    }

    public int GetAccumulateDmg()
    {
        return accumulateDmg;
    }

    public void AddAccumulateDmg(int accumulateDmg)
    {
        this.accumulateDmg += accumulateDmg;
    }

    public void ResetAccumulateDmg()
    {
        this.accumulateDmg = 0;
    }
    #endregion

    #region SkillState_Water
    [SerializeField]
    int cold = 0;
    int freeze = 0;
    int barrier = 0;
    public void ChangeCold(int stack)
    {
        cold += stack;
    }

    public int GetCold()
    {
        return cold;
    }

    public void ChangeFreeze(bool freeze)
    {
        if (freeze)
        {
            this.freeze++;
        }
        else
        {
            this.freeze--;
        }
    }

    public bool IsFreeze()
    {
        if (freeze > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddBarrier(int barrier)
    {
        this.barrier += barrier;
    }

    public int DeleteBarrier(int barrier)
    {
        int temp = this.barrier - barrier;
        if(this.barrier < 0)
        {
            this.barrier = 0;
            return -1*temp;
        }
        return 0;
    }

    public bool HasBarrier()
    {
        if(barrier <= 0)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region Charcter Condition
    public bool IsStun()
    {
        if(_stun > 0)
        {
            return true;
        }
        return false;
    }
    public bool IsShackle ()
    {
        if (_shackle > 0)
        {
            return true;
        }
        return false;
    }
    public bool IsSilence()
    {
        if (_silence > 0)
        {
            return true;
        }
        return false;
    }
    public int GetSlow()
    {
        return _slow;
    }

    public void ChangeStun(bool stun)
    {
        if(stun == true)
        {
            _stun++;
        }
        else
        {
            _stun--;
        }
    }
    public void ChangeShackle(bool shackle)
    {
        if (shackle == true)
        {
            _shackle++;
        }
        else
        {
            _shackle--;
        }
    }
    public void ChangeSilence(bool silence)
    {
        if (silence == true)
        {
            _silence++;
        }
        else
        {
            _silence--;
        }
    }
    public void ChangeSlow(bool state, int slow)
    {
        if (state == true)
        {
            _slow += slow;
            this.gameObject.GetComponent<EntityStat>().MovePoint -= slow;
        }
        else
        {
            _slow -= slow;
            this.gameObject.GetComponent<EntityStat>().MovePoint += slow;
        }
    }
    #endregion
    #region Debuff
    public void AddDebuff(Debuff debuff, bool turnEnd = false)
    {
        if (turnEnd == false)
        {
            debuffs.Add(debuff);
        }
        else
        {
            addDebuffList.Add(debuff);
        }
    }

    public void DelDebuff(Debuff debuff)
    {
        deleteDebuffList.Add(debuff);
    }

    public Debuff FindDebuff(Debuff debuff)
    {
        if (debuff == null) return null;
        foreach (Debuff de in debuffs)
        {
            if (de.GetType() == typeof(Debuff))
                return de;
        }
        return null;
    }
    #endregion
    #region Buff
    public void AddBuff(Buff buff, bool turnEnd = false)
    {
        if (turnEnd == false)
        {
            buffs.Add(buff);
        }
        else
        {
            addBuffList.Add(buff);
        }
    }

    public void DelBuff(Buff buff)
    {
        deleteBuffList.Add(buff);
    }

    public Buff FindBuff(Buff buff)
    {
        if (buff == null) return null;
        foreach (Buff bu in buffs)
        {
            if (bu.GetType() == typeof(Buff))
                return bu;
        }
        return null;
    }
    #endregion

    public void ClearBuff_Debuff()
    {
        foreach (Debuff debuff in deleteDebuffList)
        {
            debuffs.Remove(debuff);
        }

        foreach (Buff buff in deleteBuffList)
        {
            buffs.Remove(buff);
        }

        deleteBuffList.Clear();
        deleteDebuffList.Clear();

        foreach (Debuff debuff in addDebuffList)
        {
            debuffs.Add(debuff);
        }

        foreach (Buff buff in addBuffList)
        {
            buffs.Add(buff);
        }

        addBuffList.Clear();
        addDebuffList.Clear();
    }

    public void CalcTurn()
    {
        if (debuffs.Count > 0)
        {
            foreach (Debuff debuff in debuffs)
            {
                debuff.TimeCheck();
            }
        }

        if (buffs.Count > 0)
        {
            foreach (Buff buff in buffs)
            {
                buff.TimeCheck();
            }
        }

        ClearBuff_Debuff();
    }
}
