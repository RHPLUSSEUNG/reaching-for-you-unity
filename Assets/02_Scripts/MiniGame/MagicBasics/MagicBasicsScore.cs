using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MagicBasicsScore : MonoBehaviour
{
    static MagicBasicsScore instance = null;
    public static MagicBasicsScore Instance { get { return instance; } }

    [SerializeField] Slider scoreSlider = null;

    [SerializeField] float increaseScore = 1;   
    float totalScore = 100f; 
    [SerializeField] float[] weight = null;

    float currentScore = 0;
    
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    void Start()
    {
        currentScore = 0;
    }

    // 점수 위치 (-247, 247)
    public void IncreaseScore(int judgementId)
    {
        float temp = increaseScore;

        // 가중치 계산
        temp = temp * weight[judgementId];
        currentScore += temp;

        // Slider에 반영
        scoreSlider.value = currentScore / totalScore;
    }

    public void Ranking()
    {
        if(currentScore >= 80)
        {
            Debug.Log("A");
        }
        else if(currentScore >= 60)
        {
            Debug.Log("B");
        }
        else if(currentScore >= 40)
        {
            Debug.Log("C");
        }
        else if(currentScore >= 20)
        {
            Debug.Log("D");
        }
        else
        {
            Debug.Log("F");
        }
    }
}
