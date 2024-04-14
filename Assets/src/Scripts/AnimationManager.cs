using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool playAnimation = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (playAnimation && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            playAnimation = false;
            animator.SetTrigger("PlayCycle");
        }
    }
}
