using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MagicBasicsScore : MonoBehaviour
{
    static MagicBasicsScore instance = null;
    public static MagicBasicsScore Instance { get { return instance; } }

    private bool isPlaying = true;
    public bool IsPlaying { 
        get { return isPlaying; }
        set { isPlaying = value; }
    }

    [SerializeField] Slider scoreSlider = null;

    [SerializeField] float increaseScore = 1;   
    float totalScore = 100f; 
    [SerializeField] float[] weight = null;

    [SerializeField] Image scoreDecoImage;
    [SerializeField] Sprite[] scoreDecoSprite;
    [SerializeField] Image rankImage;
    [SerializeField] Sprite[] rankSprite;

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
        ChangeScoreImage(scoreSlider.value);
    }

    private void ChangeScoreImage(float scoreValue)
    {
        if(scoreValue >= 0.76f)
        {
            scoreDecoImage.sprite = scoreDecoSprite[0];
        }
        else if(scoreValue >= 0.5f)
        {
            scoreDecoImage.sprite = scoreDecoSprite[1];
        }
        else if(scoreValue >= 0.23f)
        {
            scoreDecoImage.sprite = scoreDecoSprite[2];
        }
        else
        {
            scoreDecoImage.sprite = scoreDecoSprite[3];
        }
    }

    public void RankingUI()
    {
        if((currentScore / totalScore) >= 0.76f)
        {
            rankImage.sprite = rankSprite[0];
        }
        else if((currentScore / totalScore) >= 0.5f)
        {
            rankImage.sprite = rankSprite[1];
        }
        else if((currentScore / totalScore) >= 0.23f)
        {
            rankImage.sprite = rankSprite[2];
        }
        else
        {
            rankImage.sprite = rankSprite[3];
        }
    }
}
