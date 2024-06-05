using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    RangeDetector detector;
    GameObject effect;
    bool is_effect;
    void Start()
    {
        effect = GameObject.Find("ElectricShockConfirmed");


    }

    void Update()
    {
        if (!is_effect)
        {
            StartCoroutine(StartEffect(effect, new Vector3 (1,1,1)));
        }
    }

    public IEnumerator StartEffect(GameObject effect, Vector3 pos)
    {
        effect.transform.position = pos;
        effect.GetComponent<ParticleSystem>().Play();
        is_effect = true;
        while (effect.GetComponent<ParticleSystem>().isPlaying)
        {
            Debug.Log(effect);
            yield return null;
        }
        is_effect = false;
        yield break;
    }
}
