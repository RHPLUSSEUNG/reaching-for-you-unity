using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcterDataHandler : MonoBehaviour
{
    [SerializeField] AMCharacterData characterData;

    public AMCharacterData GetCharacterData()
    {
        return characterData;
    }
}
