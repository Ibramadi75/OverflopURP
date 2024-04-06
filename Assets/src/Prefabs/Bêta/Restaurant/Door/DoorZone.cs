using UnityEngine;

public class DoorZone : MonoBehaviour
{
    Animation animation;
    public float speed = 3f;
    string currentAnimationName;
    public string openAnimationToPlay;
    public string closeAnimationToPlay;

    void Start()
    {
        animation = GetComponentInParent<Animation>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentAnimationName != "Open" && currentAnimationName != "Open1")
        {
            animation[openAnimationToPlay].speed = speed;
            animation.Play(openAnimationToPlay);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentAnimationName != "Close" && currentAnimationName != "Close1")
        {
            animation[closeAnimationToPlay].speed = speed;
            animation.Play(closeAnimationToPlay);
        }
    }

    void Update()
    {
        if (animation.isPlaying)
        {
            currentAnimationName = GetCurrentAnimationName();
        }
    }
    
    string GetCurrentAnimationName()
    {
        foreach (AnimationState state in animation)
        {
            if (animation.IsPlaying(state.name))
            {
                return state.name;
            }
        }
        return "Aucune animation en cours de lecture";
    }
}
