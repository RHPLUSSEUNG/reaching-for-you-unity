using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureEncounterTrigger : MonoBehaviour
{
    private void OnDisable()
    {
        AdventureManager.adventure.EncounterChance();
    }
}
