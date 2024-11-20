using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSpriteAnimation : MonoBehaviour
{
    [SerializeField]
    List<Sprite> idleAnim = new List<Sprite>();
    [SerializeField]
    List<Sprite> runAnim = new List<Sprite>();
    [SerializeField]
    List<Sprite> hitAnim = new List<Sprite>();

    public bool isIdle = false;
    public bool isRun = false;
    public bool isHit = false;

    float totalTime;
    float idleTime;
    float runTime;
    float hitTime = 1.0f;
    public void PlayIdleAnim()
    {
        Image img = GetComponent<Image>();
        for(int i = 0; i < idleAnim.Count; i++)
        {
            img.sprite = idleAnim[i];
        }
    }

    public void PlayRunAnim()
    {
        Image img = GetComponent<Image>();
        for(int i =0; i < runAnim.Count;i++)
        {
            img.sprite = runAnim[i];
        }
    }

    public void PlayHitAnim()
    {
        Image img = GetComponent<Image>();
        for (int i = 0; i < hitAnim.Count; i++)
        {
            img.sprite = hitAnim[i];
        }
    }

    public void StartAnim(float idle, float run, float hit = 1.0f, float total = 20.0f)
    {
        totalTime = total;
        idleTime = idle;
        runTime = run;
        hitTime = hit;
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim()
    {
        Image img = GetComponent<Image>();
        while (true)
        {
            if(isIdle)
            {
                for (int i = 0; i < idleAnim.Count; i++)
                {
                    img.sprite = idleAnim[i];
                }
            }
            if(isRun)
            {
                for (int i = 0; i < runAnim.Count; i++)
                {
                    img.sprite = runAnim[i];
                }
            }
            if(isHit)
            {
                for (int i = 0; i < hitAnim.Count; i++)
                {
                    img.sprite = hitAnim[i];
                }
            }
        }
    }
}
