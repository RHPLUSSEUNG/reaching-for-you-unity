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

    //current buff & debuff
    public List<Buff> buffs = new();
    public List<Debuff> debuffs = new();
    //delete when turn end
    private List<Buff> deleteBuffList = new();
    private List<Debuff> deleteDebuffList = new();
    //create when turn start
    private List<Buff> addBuffList = new();
    private List<Debuff> addDebuffList = new();

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
    public void AddDebuff(Debuff debuff, bool turnEnd)
    {
        if (turnEnd == false)
        {
            Debuff cur = FindDebuff(debuff);
            if (cur != null)
            {
                cur.Duplicate_Debuff(debuff);
            }
            else
            {
                debuffs.Add(debuff);
            }
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
    public void AddBuff(Buff buff, bool turnEnd)
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
