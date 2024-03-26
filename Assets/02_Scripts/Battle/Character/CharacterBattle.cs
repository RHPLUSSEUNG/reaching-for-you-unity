using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
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
    }

    public void Dead()
    {
        if(_spec.hp > 0)
        {
            return;
        }
        Destroy(_go);
    }

    public void Attack(GameObject target, int damage)
    {
        CharacterBattle t_battle = target.GetComponent<CharacterBattle>();
        if(t_battle == null)
        {
            return;
        }
        t_battle.GetDamaged(damage, _spec.elementType);
    }
}
