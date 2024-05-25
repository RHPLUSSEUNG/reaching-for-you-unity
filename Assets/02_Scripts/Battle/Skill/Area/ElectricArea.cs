using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArea : MonoBehaviour
{
    Collider[] colliders;
    int range = 3;
    int layerMask = 1 << 7;

    public void GetCharacter()
    {
        colliders = Physics.OverlapBox(this.transform.position, new Vector3(range / 2, 3 / 2, range / 2), Managers.Battle.currentCharacter.transform.rotation, layerMask);
    }

    public void CalcTurn()
    {
        GetCharacter();
        foreach (Collider collider in colliders)
        {
            ElectricShock shock = new();
            if(collider.gameObject == Managers.Battle.currentCharacter)
            {
                shock.SetDebuff(3, collider.gameObject, 0, true);
            }
        }
    }
}
