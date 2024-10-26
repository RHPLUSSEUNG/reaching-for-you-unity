using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBasicsScore : MonoBehaviour
{
    [SerializeField] Slider scoreSlider = null;

    [SerializeField] float increaseScore = 10;    
    [SerializeField] float[] weight = null;

    float currentScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    public void IncreaseScore(int judgementId)
    {
        float temp = increaseScore;

        // 가중치 계산
        temp = temp * weight[judgementId];
        currentScore += temp;

        // Slider에 반영
        scoreSlider.value = currentScore / 1000;
    }
}
