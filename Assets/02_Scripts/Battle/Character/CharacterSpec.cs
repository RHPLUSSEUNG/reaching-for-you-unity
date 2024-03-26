using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpec : MonoBehaviour
{
    [SerializeField]
    public int hp;
    public int attack;
    public int shield;
    public short stamina;

    public ElementType elementType;
}
