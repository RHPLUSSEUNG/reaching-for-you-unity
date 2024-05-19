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
    public int stamina = 100; //active per turn
    public int remainStamina;
    public ElementType elementType = ElementType.None;

    public List<Buff> buffs = new();
    public List<Debuff> debuffs = new();
    public GameObject pos;

}
