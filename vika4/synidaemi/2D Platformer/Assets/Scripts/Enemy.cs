using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health;

    protected Animator animator;
    protected bool isDead = false;
    protected bool isBeingHit = false;

    protected Dictionary<int, float> animationDurations = new Dictionary<int, float>();

    // Animations
    int IDLE;
    int HIT;
    int DEATH;

    int currentAnimationState;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        SetUpAnimations();
        currentAnimationState = IDLE;
    }

    protected void SetUpAnimations()
    {
        var clips = animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "EnemyIdle")
            {
                IDLE = Animator.StringToHash(clip.name);
                animationDurations.Add(IDLE, clip.length);
            }
            else if (clip.name == "EnemyDeath")
            {
                DEATH = Animator.StringToHash(clip.name);
                animationDurations.Add(DEATH, clip.length);
            }
            else if (clip.name == "EnemyHit")
            {
                HIT = Animator.StringToHash(clip.name);
                animationDurations.Add(HIT, clip.length);
            }
            else animationDurations.Add(Animator.StringToHash(clip.name), clip.length);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDead) return;
        if (collider.CompareTag("Damage"))
        {
            health--;
            if (health <= 0)
            {
                isDead = true;
                SetAnimationState(DEATH);
                Destroy(gameObject, animationDurations[DEATH]);
            }
            else
            {
                SetAnimationState(HIT);
                isBeingHit = true;
                Invoke(nameof(PlayIdle), animationDurations[HIT]);
            }
        }
    }

    void PlayIdle()
    {
        if (!isDead) SetAnimationState(IDLE);
        isBeingHit = false;
    }

    protected void SetAnimationState(int state)
    {
        if (currentAnimationState == state) return;
        animator.Play(state);
        currentAnimationState = state;
    }
}
