using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    CharacterSpec _spec;
    GameObject _go;
    
    void Start()
    {
        _go = this.gameObject;
        _spec = _go.GetComponent<CharacterSpec>();
    }

    public void GetDamaged(int damage, ElementType element)
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

    public void Dead()
    {
        if(_spec.hp > 0)
        {
            return;
        }
        BattleManager.Instance.playerLive--;
        Destroy(_go);
    }

    public void Attack(GameObject target, int damage)
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

    public void UseSkill(Active skill)
    {
        if (_spec.stamina <= 0)
        {
            return;
        }
        skill.Activate();
        _spec.remainStamina--;
    }
}
