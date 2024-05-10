using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : CharacterBattle
{
    public new void GetDamaged(int damage, ElementType element)
    {
        //Disadvantage
        if(_spec.elementType - element == 1 || _spec.elementType - element == -4)
        {
            _spec.hp -= damage * 2;
        }
        //Advantage
        else if(_spec.elementType - element == -1 || _spec.elementType - element == 4)
        {
            _spec.hp -= damage / 2;
        }
        else
        {
            _spec.hp -= damage;
        }

        if(_spec.hp <= 0)
        {
            Dead();
        }
    }

    public override void Dead()
    {
        if(_spec.hp > 0)
        {
            return;
        }
        Managers.Battle.playerLive--;
        Managers.Battle.ObjectList.Remove(this.gameObject);
        Destroy(_go);
    }

    public new void Attack(GameObject target, int damage)
    {
        if(_spec.stamina <= 0)
        {
            return;
        }
        CharacterBattle t_battle = target.GetComponent<CharacterBattle>();
        if(t_battle == null)
        {
            return;
        }
        t_battle.GetDamaged(damage, _spec.elementType);
        _spec.remainStamina--;
    }

    public new void UseSkill(Active skill, GameObject target)
    {
        if (_spec.stamina <= 0)
        {
            return;
        }
        skill.SetTarget(target);
        _spec.remainStamina--;
    }
}
