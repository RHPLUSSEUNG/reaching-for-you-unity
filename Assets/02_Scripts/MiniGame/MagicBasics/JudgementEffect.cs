using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgementEffect : MonoBehaviour
{
    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] Image judgementImage = null;
    [SerializeField] Sprite[] judgementSprites = null;

    public void JudgementAnim(int judgementId)
    {
        judgementImage.sprite = judgementSprites[judgementId];
        judgementAnimator.SetTrigger("Hit");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
