using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager
{
    #region Attribute
    Cover cover;
    #endregion
    #region Getter & Setter
    public void SetCoverData(GameObject _cover)
    {
        if(_cover == null)
        {
            cover = null;
            return;
        }
        cover = _cover.transform.parent.GetComponent<Cover>();
    }
    #endregion
    public void Dead(GameObject character)
    {
        if (character.CompareTag("Player"))
        {
            Managers.Battle.playerLive--;
        }
        else
        { 
            Managers.Battle.monsterLive--;
        }
        int dead_Index = Managers.Battle.ObjectList.IndexOf(character);         // 추가 : UI 생성
        Managers.Battle.ObjectList.Remove(character);
        character.SetActive(false);
        Managers.BattleUI.turnUI.DestroyTurnUI(character);     // 추가 : UI 생성

        #region 임시 결과 조건
        if (Managers.Battle.monsterLive == 0 || Managers.Battle.playerLive == 0)
            Managers.Battle.Result();
        #endregion
    }

    public void Damage(GameObject target)
    {

        GameObject character = Managers.Battle.currentCharacter;
        if (character.GetComponent<EntityStat>().BaseDamage <= 0)
        {
            return;
        }
        target.GetComponent<EntityStat>().Hp -= character.GetComponent<EntityStat>().BaseDamage;
        if (target.GetComponent<EntityStat>().Hp <= 0)
        {
            Dead(target);
        }
    }

    public int Damage(GameObject target, int damage, ElementType element = ElementType.None, bool close = false)
    {
        if (target == null)
            return 0;
        //Debug.Log(target);
        if (damage <= 0 || target.GetComponent<EntityStat>() == null)
        {
            return 0;
        }

        damage -= (int)target.GetComponent<EntityStat>().Defense;

        CharacterState state = target.GetComponent<CharacterState>();

        #region Cover
        if (target.CompareTag("Player") & cover != null)
        {
            if(!cover.CheckTarget(damage, Managers.Battle.currentCharacter.transform.position, target.transform.position))
                return damage;
        }
        #endregion
        if (state.GetEvasion() > 0)
        {
            int value = Random.Range(1, 101);
            if (value <= state.GetEvasion())
            {
                return 0;
            }
        }
        #region Electric
        if (element == ElementType.Electric)
        {
            state.CalcCapacity(target);
            if (state.IsElecImmune())
            {
                return 0;
            }

            if (state.IsShock() && close == true)
            {
                ElectricShock shock = new();
                shock.SetDebuff(1, Managers.Battle.currentCharacter, 1);
            }
        }
        #endregion
        #region Water
        if (state.HasBarrier())
        {
            damage = state.DeleteBarrier(damage);
            if (damage <= 0)
            {
                return 0;
            }
        }
        #endregion
        if (target.GetComponent<EnemyAI_Base>() != null)
        {
            //target.GetComponent<EntityStat>().Hp -= damage;
            target.GetComponent<EnemyAI_Base>().OnHit(damage);

            #region 임시 체크 코드 - 권희준
            if (target.GetComponent<EntityStat>().Hp <= 0)
            {
                Dead(target);
            }
            #endregion

            return damage;
        }
        else if (target.GetComponent<PlayerBattle>() != null)
        {
            //Debug.Log("Player Damaged");
            target.GetComponent<PlayerBattle>().OnHit(damage);
        }

        if (state.GetEndure())
        {
            state.AddAccumulateDmg(damage);
        }
        if (target.GetComponent<EntityStat>().Hp <= 0)
        {
            Dead(target);
        }
        return damage;
    }

    public void Heal(GameObject target, int heal)
    {
        EntityStat stat = target.GetComponent<EntityStat>();
        stat.Hp += heal;
        if (stat.Hp > stat.MaxHp)
        {
            stat.Hp = stat.MaxHp;
        }
    }

    public void MPRecovery(GameObject target, int attribute)
    {
        EntityStat stat = target.GetComponent<EntityStat>();
        stat.Mp += attribute;
        if (stat.Mp > stat.MaxMp)
        {
            stat.Mp = stat.MaxMp;
        }
    }

    public void ModifyAtk(GameObject target, int attribute)
    {
        EntityStat stat = target.GetComponent<EntityStat>();
        stat.BaseDamage += attribute;
    }

    public void ModifyDefense(GameObject target, int attribute)
    {
        EntityStat stat = target.GetComponent<EntityStat>();
        stat.Defense += attribute;
    }

    public void ModifySpeed(GameObject target, int attribute)
    {
        EntityStat stat = target.GetComponent<EntityStat>();
        stat.MovePoint += attribute;
    }
}
