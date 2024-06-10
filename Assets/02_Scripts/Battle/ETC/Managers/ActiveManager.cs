using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager
{
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
        Managers.Battle.ObjectList.Remove(character);
        character.SetActive(false);
        Managers.Battle.Result();
    }

    public void Damage(GameObject target)
    {
        
        GameObject character = Managers.Battle.currentCharacter;
        if(character.GetComponent<EntityStat>().BaseDamage <= 0)
        {
            return;
        }
        target.GetComponent<EntityStat>().Hp -= character.GetComponent<EntityStat>().BaseDamage;
        if (target.GetComponent<EntityStat>().Hp <= 0)
        {
            Dead(target);
        }
    }

    public void Damage(GameObject target, int damage, ElementType element = ElementType.None, bool close = false)
    {
        Debug.Log(target);
        if (damage <= 0)
        {
            return;
        }

        CharacterState state = target.GetComponent<CharacterState>();

        #region Electric
        if (element == ElementType.Electric)
        {
            state.GetCapacity(target);
            if (state.GetElecImmune())
            {
                return;
            }

            if (state.GetShock() && close == true)
            {
                ElectricShock shock = new();
                shock.SetDebuff(1, Managers.Battle.currentCharacter, 1);
            }
        }
        #endregion

        if (target.GetComponent<EnemyAI_Base>() != null)
        {
            target.GetComponent<EnemyAI_Base>().OnHit(damage);
            return;
        }else if (target.GetComponent<PlayerBattle>() != null)
        {
            Debug.Log("Player Damaged");
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
