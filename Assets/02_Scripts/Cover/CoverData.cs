using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoverData", menuName = "Reaching-for-you/CoverData")]
public class CoverData : ScriptableObject
{
    [SerializeField]
    private int step;

    // 단계 별 엄폐물
    public GameObject coverGameObject;

    private float damagePercent;
    public void Init() {
        switch (step) 
        {
            case 0:
                damagePercent = 0;
                break;
            case 1:
                damagePercent = 0.2f;
                break;
            case 2:
                damagePercent = 0.6f;
                break;
            case 3:
                damagePercent = 1f;
                break;
        }
    }
}
