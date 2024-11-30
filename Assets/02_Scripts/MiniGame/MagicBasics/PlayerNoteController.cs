using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoteController : MonoBehaviour
{
    TimingManager timingManager;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    ParticleSystem hit_magic;

    void Start()
    {
        timingManager = FindObjectOfType<TimingManager>();    
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && MagicBasicsScore.Instance.IsPlaying)
        {
            timingManager.CheckTiming();
            anim.SetTrigger("Hit");
            hit_magic.Play();
        }
    }
}
