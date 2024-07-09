using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    public GameObject[] coverPrefab;

    int maxStep = 3;
    int step;

    float damagePercent;

    private void Awake() {
        setStep();
    }

    private void setStep() 
    {
        step = Random.Range(0, maxStep + 1);

        GameObject coverObject;
        switch (step)
        {
            case 0:
            case 1:
                damagePercent = 0.8f;
                coverObject = Instantiate(coverPrefab[0], transform);
                coverObject.transform.SetParent(transform.parent);
                break;
            case 2:
                damagePercent = 0.5f;
                coverObject = Instantiate(coverPrefab[1], transform);
                coverObject.transform.SetParent(transform.parent);
                break;
            case 3:
                damagePercent = 0f;
                coverObject = Instantiate(coverPrefab[2], transform);
                coverObject.transform.SetParent(transform.parent);
                break;
        }
    }
}
