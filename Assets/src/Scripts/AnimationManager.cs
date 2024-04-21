using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool playAnimation;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (playAnimation && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            playAnimation = false;
            animator.SetTrigger("PlayCycle");
        }
    }
}