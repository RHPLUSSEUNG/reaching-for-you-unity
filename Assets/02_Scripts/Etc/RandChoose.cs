using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandChoose : MonoBehaviour
{

    public Dictionary<string, float> elements;

    public string GetRandChoose(Dictionary<string, float> dic)
    {
        float randValue = Random.Range(0f, 1f);
        float sum = 0;
        foreach (KeyValuePair<string, float> kvp in dic)
        {
            sum += kvp.Value;
            if (randValue <= sum)
                return kvp.Key;
        }
        return null;
    }
}
