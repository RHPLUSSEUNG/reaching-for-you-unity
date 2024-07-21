using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : CoverData
{
    public GameObject[] coverPrefab;

    GameObject coverObject;

    private void Awake() {
        setStep();
    }

    private void setStep() 
    {
        step = Random.Range(0, maxStep + 1);
        
        switch (step)
        {
            case 0:
            case 1:
                damagePercent = 0.8f;
                hp = 40;
                coverObject = Instantiate(coverPrefab[0], transform);
                coverObject.transform.SetParent(transform.parent);
                break;
            case 2:
                damagePercent = 0.5f;
                hp = 70;
                coverObject = Instantiate(coverPrefab[1], transform);
                coverObject.transform.SetParent(transform.parent);
                break;
            case 3:
                damagePercent = 0f;
                hp = 100;
                coverObject = Instantiate(coverPrefab[2], transform);
                coverObject.transform.SetParent(transform.parent);
                break;
        }
    }

    void Update()
    {
        UpdatePlayerPosition(coverObject.transform.position);
    }
}
