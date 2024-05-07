using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpec : MonoBehaviour
{
    [SerializeField]
    public int hp; //character hp
    public int mp;
    public int attack; //character attack damage
    public int shield; //character shield damage
    public short stamina; //active per turn
    public short remainStamina;
    public ElementType elementType;

    public List<Debuff> debuffs;
    public List<Buff> buffs;

    public GameObject pos;
}
