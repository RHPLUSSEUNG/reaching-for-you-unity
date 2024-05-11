using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpec : MonoBehaviour
{
    [SerializeField]
    public int hp = 100; //character hp
    public int mp = 50;
    public int attack = 50; //character attack damage
    public int shield; //character shield damage
    public short stamina; //active per turn
    public short remainStamina;
    public ElementType elementType;

    public List<Debuff> debuffs;
    public List<Buff> buffs;

    public GameObject pos;

}
